using Models.Persons;
using Models.Requests.Persons.Phone;
using Models.Requests.Persons.RelatedPersons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Requests.Persons
{
    public class CreatePersonRequest
    {
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        public string Gender { get; set; }
        [StringLength(11, MinimumLength = 11)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Identification number must be numeric")]
        public string IdentificationNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public List<CreatePhoneRequest> Phones { get; set; }
        public string Image { get; set; }
        public List<CreateRelatedPersonRequest> RelatedPersons { get; set; }
    }
}
