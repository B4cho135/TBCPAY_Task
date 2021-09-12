using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ActionFilters
{
    public class GeneralValidationAttribute : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var model = JObject.Parse(JsonConvert.SerializeObject(context.ActionArguments["model"]));

            var BirthDate = (DateTime?)model["BirthDate"];

            var age = (DateTime.Now.Year - BirthDate.Value.Year);

            if (BirthDate.HasValue && BirthDate.Value.Month > DateTime.Now.Month)
            {
                 age--;
            }

            if(age < 18)
            {
                context.Result = new BadRequestObjectResult("Person should be at least 18 years old!");
                return;
            }

            if(!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        
    }
}
