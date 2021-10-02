using AspNetWebApi.API.DTOs;
using AspNetWebApi.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebApi.API.Filters
{
    public class NotFoundFilter : ActionFilterAttribute
    {
        private readonly IProductService _productService;
        
        public NotFoundFilter(IProductService productService)
        {
            _productService = productService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault(); //Eğer id'nin ismini almak istersek Values yerine key yazmamız gerekir
            var product = await _productService.GetByIdAsync(id);
            if (product != null)
            {
                await next(); //Bir sonraki adıma aktar
            }
            else
            {
                ErrorDto dto = new ErrorDto();
                dto.StatusCode = 404;
                dto.Errors.Add($"Id'si {id} olan ürün veritabanında bulunamadı.");
                context.Result = new NotFoundObjectResult(dto);
            }
        }

    }
}
