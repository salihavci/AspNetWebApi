using AspNetWebApi.Core.Models;
using AspNetWebApi.Core.Services;
using AspNetWebApi.Web.DTOs;
using AspNetWebApi.Web.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebApi.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAllAsync().Result; //Asenkron method ama async yazmamak için result yazıldı.
            return View(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto dto)
        {
            await _categoryService.AddAsync(_mapper.Map<Category>(dto));
            return RedirectToAction("Index", "Categories");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = _categoryService.GetByIdAsync(id).Result;
            return View(_mapper.Map<CategoryDto>(category));
        }

        [HttpPost]
        public IActionResult Update(CategoryDto dto)
        {
            _categoryService.Update(_mapper.Map<Category>(dto));
            return RedirectToAction("Index", "Categories");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetByIdAsync(id).Result;
            _categoryService.Remove(category);
            return RedirectToAction("Index", "Categories");
        }
    }
}
