using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PersonEntity : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public CityEntity Cities { get; set; }
        public ICollection<PhoneEntity> Phones { get; set; }
        public string Image { get; set; }
        public ICollection<RelatedPersonEntity> RelatedPersons { get; set; }
    }
}
