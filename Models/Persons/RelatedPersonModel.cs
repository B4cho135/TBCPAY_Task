using Models.Requests.Persons.RelatedPersons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Persons
{
    public class RelatedPersonModel
    {
        public RelationTypeModel RelationType { get; set; }
        public PersonModel RelatedPerson { get; set; }
    }
}
