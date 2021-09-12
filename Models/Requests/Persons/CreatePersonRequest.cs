using Models.Persons;
using Models.Requests.Persons.Phone;
using Models.Requests.Persons.RelatedPersons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Requests.Persons
{
    public class CreatePersonRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public List<PhoneRequest> Phones { get; set; }
        public string Image { get; set; }
        public List<RelatedPersonRequest> RelatedPersons { get; set; }
    }
}
