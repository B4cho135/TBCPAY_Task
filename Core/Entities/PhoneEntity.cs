using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PhoneEntity : BaseEntity<int>
    {
        public PhoneEnum Type { get; set; }
        public string PhoneNumber { get; set; }
    }
}
