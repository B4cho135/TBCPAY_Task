using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Persons;
using Models.Requests.Persons;
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
        public PersonsController(IPersonService personService, IRelatedPersonService relatedPersonService)
        {
            _personService = personService;
            _relatedPersonService = relatedPersonService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var persons = await _personService.GetAll();

            return Ok(persons);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var person = await _personService.GetByIdAsync(Id);

            if (person == null)
            {
                return BadRequest($"Could not find any person with Id - {Id}");

            }

            return Ok(person);

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

        [HttpPut("Id")]
        public async Task<IActionResult> Update(int Id, UpdatePersonRequest model)
        {
            return Ok();
        }

        [HttpDelete("Id")]
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
    }
}
