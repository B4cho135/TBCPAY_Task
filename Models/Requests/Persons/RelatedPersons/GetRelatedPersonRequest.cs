using Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Requests.Persons.RelatedPersons
{
    public class GetRelatedPersonRequest
    {
        public RelationTypeModel RelationType { get; set; }
        public PersonModel RelatedPerson { get; set; }
    }
}
