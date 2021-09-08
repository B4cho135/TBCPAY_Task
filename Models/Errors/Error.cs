using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses.Errors
{
    public class Error
    {
        public string Description { get; set; }
        public Error()
        {

        }
        public Error(string Description)
        {
            this.Description = Description;
        }
    }
}
