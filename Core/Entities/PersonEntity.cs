using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class PersonEntity : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public CityEntity City { get; set; }
        public int CityId { get; set; }
        public ICollection<PhoneEntity> Phones { get; set; }
        public string Image { get; set; }
        public ICollection<RelatedPersonEntity> RelatedPersons { get; set; }
    }
}
