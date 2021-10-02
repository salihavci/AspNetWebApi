using AspNetWebApi.Core.Models;
using AspNetWebApi.Core.Services;
using AspNetWebApi.Web.ApiServices;
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
        private readonly ApiService _apiService;

        public CategoriesController(ICategoryService categoryService, IMapper mapper,ApiService apiService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            var categories = _apiService.GetAllAsync().Result; //Asenkron method ama async yazmamak için result yazıldı.
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
            await _apiService.AddAsync(dto);
            return RedirectToAction("Index", "Categories");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _apiService.GetByIdAsync(id);
            return View(_mapper.Map<CategoryDto>(category));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto dto)
        {
            await _apiService.Update(dto);
            return RedirectToAction("Index", "Categories");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Delete(int id)
        {
            await _apiService.Remove(id);
            return RedirectToAction("Index", "Categories");
        }
    }
}
