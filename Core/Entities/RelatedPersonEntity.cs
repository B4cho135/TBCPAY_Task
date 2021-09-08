using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class RelatedPersonEntity : BaseEntity<int>
    {
        public RelationEnum RelationType { get; set; }
        public PersonEntity Person { get; set; }
        public int PersonId { get; set; }
    }
}
