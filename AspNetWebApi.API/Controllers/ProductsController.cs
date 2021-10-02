using AspNetWebApi.API.DTOs;
using AspNetWebApi.API.Filters;
using AspNetWebApi.Core.Models;
using AspNetWebApi.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Manuel hata fırlatmak için yazılacak kod (throw new Exception("Hatalı işlemler mevcut."));
            var products = await _productService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var products = await _productService.GetByIdAsync(id);
            return Ok(_mapper.Map<ProductDto>(products));
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductDto productDto)
        {
            var newProduct = await _productService.AddAsync(_mapper.Map<Product>(productDto));
            return Created(string.Empty,_mapper.Map<ProductDto>(newProduct));
        }

        [HttpPut]
        public IActionResult UpdateProduct(ProductDto productDto)
        {
            var newProduct = _productService.Update(_mapper.Map<Product>(productDto));
            return NoContent();
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            _productService.Remove(product);
            return NoContent();
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}/categories")]
        public async Task<IActionResult> GetWithCategoryById(int id)
        {
            var product = await _productService.GetWithCategoryByIdAsync(id);
            return Ok(_mapper.Map<ProductWithCategoryDto>(product));
        }
    }
}
