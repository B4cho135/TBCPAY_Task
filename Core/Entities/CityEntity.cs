using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CityEntity : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
