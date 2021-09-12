using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Queries.PersonQueries
{
    public class PersonSearchQuery
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityId { get; set; }
        public string Phone{ get; set; }
        public string Image { get; set; }
        public int? RelatedPersonId { get; set; }
    }
}
