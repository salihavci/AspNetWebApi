using AspNetWebApi.Core.Models;
using AspNetWebApi.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IService<Person> _personService;
        private readonly IMapper _mapper;

        public PersonsController(IService<Person> personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var person = await _personService.GetAllAsync();
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Person person)
        {
            var newPerson = await _personService.AddAsync(person);
            return Created(string.Empty, newPerson);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _personService.GetByIdAsync(id);
            return Ok(person);
        }

        [HttpPut]
        public IActionResult Update(Person person)
        {
            var newPerson = _personService.Update(person);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var person = await _personService.GetByIdAsync(id);
            _personService.Remove(person);
            return NoContent();
        }
    }
}
