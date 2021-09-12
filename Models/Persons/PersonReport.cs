using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Persons
{
    public class PersonReport
    {
        public int PersonId { get; set; }
        public ReportByRelatedPersonType QuantityOfPersonsRelatedByType { get; set; }
    }
}
