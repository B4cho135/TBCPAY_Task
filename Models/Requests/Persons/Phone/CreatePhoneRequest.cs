using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Requests.Persons.Phone
{
    public class CreatePhoneRequest
    {
        public int TypeId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
