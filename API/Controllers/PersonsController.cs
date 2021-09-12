using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Persons;
using Models.Queries.PersonQueries;
using Models.Requests.Persons;
using Models.Requests.Persons.Phone;
using Models.Requests.Persons.RelatedPersons;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IRelatedPersonService _relatedPersonService;
        private readonly IPhoneService _phoneService;
        protected readonly IMapper _mapper;
        public PersonsController(IPersonService personService, IRelatedPersonService relatedPersonService, IMapper mapper)
        {
            _personService = personService;
            _relatedPersonService = relatedPersonService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(PersonSearchQuery query, int startPage, int Limit)
        {
            var persons =  _personService.Get().Include(x => x.RelatedPersons).Include(x => x.Phones);

            if(!string.IsNullOrEmpty(query.FirstName))
            {
                persons.Where(x => x.FirstName == query.FirstName);
            }
            if (!string.IsNullOrEmpty(query.LastName))
            {
                persons.Where(x => x.LastName == query.LastName);
            }

            if (!string.IsNullOrEmpty(query.IdentificationNumber))
            {
                persons.Where(x => x.IdentificationNumber == query.IdentificationNumber);
            }

            if (!string.IsNullOrEmpty(query.Gender))
            {
                persons.Where(x => x.Gender == query.Gender);
            }

            if (query.BirthDate.HasValue)
            {
                persons.Where(x => x.BirthDate == query.BirthDate.Value);
            }

            if (query.CityId.HasValue)
            {
                persons.Where(x => x.CityId == query.CityId.Value);
            }
            if (query.RelatedPersonId.HasValue)
            {
                persons.Where(x => x.RelatedPersons.Select(k => k.Id).Contains(query.RelatedPersonId.Value));
            }

            if (!string.IsNullOrEmpty(query.Phone))
            {
                persons.Where(x => x.Phones.Select(x => x.PhoneNumber).Contains(query.Phone));
            }


            var filteredPersons = await  persons.Skip(startPage).Take(Limit).ToListAsync();

            var mappedPersons = _mapper.Map<List<PersonModel>>(filteredPersons);


            return Ok(mappedPersons);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var person = await _personService.Get().Include(x => x.Phones).Include(x => x.RelatedPersons).FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == Id);

            if (person == null)
            {
                return BadRequest($"Could not find any person with Id - {Id}");

            }

            var mappedPerson = _mapper.Map<PersonModel>(person);

            return Ok(mappedPerson);

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonRequest model)
        {
            var newPerson = new PersonEntity()
            {
                BirthDate = model.BirthDate,
                CityId = model.CityId,
                FirstName = model.FirstName,
                IdentificationNumber = model.IdentificationNumber,
                LastName = model.LastName
            };

            var personServiceResponse = await _personService.AddAsync(newPerson);

            if (!personServiceResponse.HasSucceeded)
            {
                return BadRequest(personServiceResponse.Message);
            }

            foreach (var relatedPerson in model.RelatedPersons)
            {
                var newRelation = new RelatedPersonEntity()
                {
                    PersonId = personServiceResponse.Item.Id,
                    RelationTypeId = relatedPerson.RelationTypeId
                };

                var relatedPersonServiceResponse = await _relatedPersonService.AddAsync(newRelation);

                if (!relatedPersonServiceResponse.HasSucceeded)
                {
                    //log why
                }

            }

            foreach (var phone in model.Phones)
            {

                var alreadyExists = _phoneService.Get().FirstOrDefault(x => x.IsDeleted == false && x.PhoneNumber == phone.PhoneNumber) != null;

                if(alreadyExists)
                {
                    continue;
                }

                var newPhone = new PhoneEntity()
                {
                    TypeId = phone.TypeId,
                    PersonId = personServiceResponse.Item.Id,
                    PhoneNumber = phone.PhoneNumber
                };
                var phoneServiceResponse = await _phoneService.AddAsync(newPhone);

                if (!phoneServiceResponse.HasSucceeded)
                {
                    //log why
                }
            }

            return Created(Request.Path, personServiceResponse.Item.Id);

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, UpdatePersonRequest model)
        {

            var person = await _personService.Get().FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == Id);

            if(person == null)
            {
                return BadRequest($"person with Id - {Id} was not found to update");
            }

            if(person.Gender != "Male" && person.Gender != "Female")
            {
                return BadRequest("Gender should be either Male or Female");
            }


            person.IdentificationNumber = model.IdentificationNumber;
            person.Image = model.Image;
            person.FirstName = model.FirstName;
            person.LastName = model.LastName;
            person.CityId = model.CityId;
            person.BirthDate = model.BirthDate;
            person.Gender = model.Gender;

            var personServiceResponse = await _personService.UpdateAsync(person);

            if(!personServiceResponse.HasSucceeded)
            {
                return BadRequest(personServiceResponse.Message);
            }

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var person = _personService.Get().FirstOrDefault(x => x.Id == Id);

            if(person == null)
            {
                return BadRequest($"There is no person with Id - {Id} to be deleted!");
            }


            person.DeletedAt = DateTime.Now;
            person.IsDeleted = true;

            //this is hard delete, I'm using soft delete using Update Method.
            //var personServiceResponse = await _personService.DeleteByIdAsync(Id); 

            var personServiceResponse = await _personService.UpdateAsync(person);

            if(!personServiceResponse.HasSucceeded)
            {
                return BadRequest(personServiceResponse.Message);
            }

            return NoContent();
        }



        [HttpGet("{Id}/RelatedPersons")]
        public async Task<IActionResult> GetRelatedPersons(int Id)
        {
            var relatedPersons = await _relatedPersonService.Get().Where(x => x.IsDeleted == false && x.PersonId == Id).ToListAsync();

            var relatedPersonsMapped = _mapper.Map<List<GetRelatedPersonRequest>>(relatedPersons);

            return Ok(relatedPersonsMapped);

        }

        [HttpPost("{Id}/RelatedPersons")]
        public async Task<IActionResult> AddRelatedPerson(int Id, CreateRelatedPersonRequest model)
        {
            var newRelatedPerson = new RelatedPersonEntity()
            {
                PersonId = model.PersonId,
                RelationTypeId = model.RelationTypeId
            };

            var relatedPersonServiceRespnse = await _relatedPersonService.AddAsync(newRelatedPerson);

            if(!relatedPersonServiceRespnse.HasSucceeded)
            {
                return BadRequest(relatedPersonServiceRespnse.Message);
            }

            var mappedRelatedPerson = _mapper.Map<RelatedPersonModel>(relatedPersonServiceRespnse.Item);

            return Created(Request.Path, mappedRelatedPerson);
        }

        [HttpDelete("{Id}/RelatedPersons/{relatedPersonId}")]
        public async Task<IActionResult> DeleteRelatedPerson(int Id, int relatedPersonId)
        {
            var person = _personService.Get().Include(x => x.RelatedPersons).FirstOrDefault(x => !x.IsDeleted && x.Id == Id);

            var relatedPerson = person.RelatedPersons.FirstOrDefault(x => !x.IsDeleted && x.PersonId == relatedPersonId);

            if (relatedPerson != null)
            {
                relatedPerson.IsDeleted = true;
                relatedPerson.DeletedAt = DateTime.Now;

                var relatedPersonServiceResponse = await _relatedPersonService.UpdateAsync(relatedPerson);

                if(!relatedPersonServiceResponse.HasSucceeded)
                {
                    return BadRequest(relatedPersonServiceResponse.Message);
                }

                return Ok();
            }

            return NotFound();

        }


        [HttpPost("{Id}/Phones")]
        public async Task<IActionResult> AddPhone(int Id, CreatePhoneRequest model)
        {
            var newRelatedPerson = new PhoneEntity()
            {
                PersonId = Id,
                PhoneNumber = model.PhoneNumber,
                TypeId = model.TypeId
            };

            var phoneServiceRespnse = await _phoneService.AddAsync(newRelatedPerson);

            if (!phoneServiceRespnse.HasSucceeded)
            {
                return BadRequest(phoneServiceRespnse.Message);
            }

            var mappedRelatedPerson = _mapper.Map<PhoneModel>(phoneServiceRespnse.Item);

            return Created(Request.Path, mappedRelatedPerson);
        }

        [HttpDelete("{Id}/RelatedPersons/{phoneId}")]
        public async Task<IActionResult> DeletePhone(int Id, int phoneId)
        {
            var person = _personService.Get().Include(x => x.Phones).FirstOrDefault(x => !x.IsDeleted && x.Id == Id);

            var phone = person.Phones.FirstOrDefault(x => !x.IsDeleted && x.PersonId == Id && x.Id == phoneId);

            if (phone != null)
            {
                phone.IsDeleted = true;
                phone.DeletedAt = DateTime.Now;

                var phoneServiceResponse = await _phoneService.UpdateAsync(phone);

                if (!phoneServiceResponse.HasSucceeded)
                {
                    return BadRequest(phoneServiceResponse.Message);
                }

                return Ok();
            }

            return NotFound();

        }

        [HttpPut("{Id}/RelatedPersons/{phoneId}")]
        public async Task<IActionResult> UpdatePhone(int Id, int phoneId, UpdatePhoneRequest model)
        {
            var person = _personService.Get().Include(x => x.Phones).FirstOrDefault(x => !x.IsDeleted && x.Id == Id);

            var phone = person.Phones.FirstOrDefault(x => !x.IsDeleted && x.PersonId == Id && x.Id == phoneId);

            if (phone != null)
            {
                phone.PhoneNumber = model.PhoneNumber;
                phone.TypeId = model.TypeId;

                var phoneServiceResponse = await _phoneService.UpdateAsync(phone);

                if (!phoneServiceResponse.HasSucceeded)
                {
                    return BadRequest(phoneServiceResponse.Message);
                }

                return Ok();
            }

            return NotFound();
        }

    }
}
