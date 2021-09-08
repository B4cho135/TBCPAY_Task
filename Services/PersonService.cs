using AutoMapper;
using Core.Entities;
using Core.Persistance;
using Models.Persons;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PersonService : GenericService<PersonEntity, PersonModel>, IPersonService
    {
        public PersonService(DefaultDbContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
