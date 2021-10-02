using AspNetWebApi.Web.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetWebApi.Web.ApiServices;

namespace AspNetWebApi.Web.Filters
{
    public class NotFoundFilter : ActionFilterAttribute
    {
        private readonly ApiService _categoryService;

        public NotFoundFilter(ApiService categoryService)
        {
            _categoryService = categoryService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault(); //Eğer id'nin ismini almak istersek Values yerine key yazmamız gerekir
            var product = await _categoryService.GetByIdAsync(id);
            if (product != null)
            {
                await next(); //Bir sonraki adıma aktar
            }
            else
            {
                ErrorDto dto = new ErrorDto();
                dto.StatusCode = 404;
                dto.Errors.Add($"Id'si {id} olan kategori veritabanında bulunamadı.");
                context.Result = new RedirectToActionResult("Error","Home",dto);
            }
        }

    }
}
