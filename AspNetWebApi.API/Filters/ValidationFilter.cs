using AspNetWebApi.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebApi.API.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ErrorDto dto = new ErrorDto();
                dto.StatusCode = 400;
                IEnumerable<ModelError> errors = context.ModelState.Values.SelectMany(v => v.Errors);
                errors.ToList().ForEach(m => dto.Errors.Add(m.ErrorMessage));
                context.Result = new BadRequestObjectResult(dto);
            }
        }
    }
}
