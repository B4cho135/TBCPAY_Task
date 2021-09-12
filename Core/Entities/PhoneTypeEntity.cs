using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PhoneTypeEntity : BaseEntity<int>
    {
        public string Type { get; set; }
    }
}
