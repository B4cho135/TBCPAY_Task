using System;
using System.Collections.Generic;

namespace Models.Persons
{
    public class PersonModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public CityModel City { get; set; }
        public int CityId { get; set; }
        public ICollection<PhoneModel> Phones { get; set; }
        public string Image { get; set; }
        public ICollection<RelatedPersonModel> RelatedPersons { get; set; }
    }
}
