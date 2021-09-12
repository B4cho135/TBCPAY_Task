using AutoMapper;
using Core.Entities;
using Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonEntity, PersonModel>();
            CreateMap<PersonModel, PersonEntity>();
            CreateMap<RelatedPersonModel, RelatedPersonEntity>();
            CreateMap<RelatedPersonEntity, RelatedPersonModel>();
            CreateMap<PhoneEntity, PhoneModel>();
            CreateMap<PhoneModel, PhoneEntity>();

        }
    }
}
