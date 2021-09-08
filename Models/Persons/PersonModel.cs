using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Persons
{
    public class PersonModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public CityModel Cities { get; set; }
        public ICollection<PhoneEntity> Phones { get; set; }
        public string Image { get; set; }
        public ICollection<RelatedPersonEntity> RelatedPersons { get; set; }
    }
}
