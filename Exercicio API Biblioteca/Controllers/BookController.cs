using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exercicio_API_Biblioteca.Controllers
{

    [ApiController, Route ("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;

        public BookController(BookService bookService, AuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }


        [HttpGet, Route("/books")]
        public IActionResult Get()
        {
            return Ok(_bookService.Get());
        }


        [HttpGet, Route("/books/{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_bookService.Get(id));
        }


        [HttpPost, AllowAnonymous, Route("/books")]
        public IActionResult Register([FromBody] RegisterBookDTO registerBookDTO)
        {
            return Created("", _bookService.Create(registerBookDTO));

        }

        [HttpPut, AllowAnonymous, Route ("/books/{idBook}")]
        public IActionResult Update (Guid idBook, [FromBody] RegisterBookDTO registerBookDTO)
        {

            return Created("", _bookService.Update(idBook, registerBookDTO));
        }


        [HttpDelete, AllowAnonymous, Route ("/books/{id}")]
        public IActionResult Delete (Guid id)
        {
            _bookService.Delete(id);
            return Ok();
        }

    }
}
