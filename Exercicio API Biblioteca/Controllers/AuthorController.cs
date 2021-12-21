using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Enums;
using Exercicio_API_Biblioteca.Repositoy;
using Exercicio_API_Biblioteca.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exercicio_API_Biblioteca.Controllers
{

    [ApiController, Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorRepository _authorRepository;
        private readonly AuthorService _authorService;

        public AuthorController(AuthorRepository authorRepository, AuthorService authorService)
        {
            _authorRepository = authorRepository;
            _authorService = authorService;
        }


        [HttpGet, Route("/author")]
        public IActionResult Get()
        {
            return Ok(_authorService.Get());
        }

        [HttpGet, Route("/author/{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_authorService.Get(id));

        }


        [HttpPost, AllowAnonymous, Route("/author")]
        public IActionResult Register([FromBody] NewAuthorDTO newAuthorDTO)
        {
            return Created("", _authorService.Create(newAuthorDTO));

        }


        [HttpPut, AllowAnonymous, Route("/author/{id}")]
        public IActionResult Update(Guid id, [FromBody] NewAuthorDTO newAuthorDTO)
        {
            return Created("", _authorService.Update(id, newAuthorDTO));

        }

        [HttpDelete, AllowAnonymous, Route("/author/{id}")]
        public IActionResult Delete(Guid id)
        {
            _authorService.Delete(id);
            return Ok();
        }


    }



}
