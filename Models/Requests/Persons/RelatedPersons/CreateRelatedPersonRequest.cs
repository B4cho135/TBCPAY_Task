using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Requests.Persons.RelatedPersons
{
    public class CreateRelatedPersonRequest
    {
        public int RelationTypeId { get; set; }
        public int PersonId { get; set; }
    }
}
