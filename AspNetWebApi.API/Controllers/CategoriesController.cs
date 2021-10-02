using AspNetWebApi.API.DTOs;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories)); //200 durum kodu
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategoryAsync(CategoryDto categoryDto)
        {
            var category = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));
            return Created(string.Empty, _mapper.Map<CategoryDto>(category));
        }

        [HttpPut]
        public IActionResult UpdateCategory(CategoryDto categoryDto)
        {
            var category = _categoryService.Update(_mapper.Map<Category>(categoryDto));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            _categoryService.Remove(category);
            return NoContent();
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetWithProductById(int id)
        {
            var cat = await _categoryService.GetWithProductByIdAsync(id);
            return Ok(_mapper.Map<CategoryWithProductDto>(cat));
        }
    }
}
