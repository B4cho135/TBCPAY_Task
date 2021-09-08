using Models.Responses.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class Response<ItemType>
    {
        public ItemType Item { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool HasSucceeded { get; set; } = false;
        public List<Error> Errors { get; set; }
    }
}
