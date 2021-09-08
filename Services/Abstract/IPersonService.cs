using Core.Entities;
using Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IPersonService : IGenericService<PersonEntity,PersonModel>
    {

    }
}
