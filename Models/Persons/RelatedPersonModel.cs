using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Persons
{
    class RelatedPersonModel
    {
        public string RelationType { get; set; }
        public PersonModel Person { get; set; }
        public int PersonId { get; set; }
    }
}
