using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Requests.Persons.Phone
{
    public class CreatePhoneRequest
    {
        public int TypeId { get; set; }
        [StringLength(50, MinimumLength = 4)]
        public string PhoneNumber { get; set; }
    }
}
