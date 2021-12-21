using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Entidades;
using Exercicio_API_Biblioteca.Repositoy;
using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.Services
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;
        private readonly AuthorRepository _authorRepository;


        public BookService(BookRepository bookRepository, AuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;

        }


        public IEnumerable<Book> Get()
        {
            return _bookRepository.Get();
        }

        public Book Get(Guid id)
        {
            return _bookRepository.Get(id);
        }

        public RegisterBookDTO Create(Guid id, RegisterBookDTO registerBookDTO)
        {
            var bookExist = _bookRepository.GetByUsername(registerBookDTO.Title);
            if (bookExist != null)
                throw new Exception("Livro já cadastrado");


            var author = _authorRepository.Get(id);


            var newBook = new Book
            {

                AuthorId = author.Id,
                AuthorName = author.NameAuthor,
                Title = registerBookDTO.Title,
                Abstract = registerBookDTO.Abstract,
                Quantity = registerBookDTO.Quantity


            };

            _bookRepository.Create(newBook);

            var newBookList = new BookList
            {
                Id = newBook.Id,
                Title = registerBookDTO.Title,
                
            };


            author.BooksList.Add(newBookList);


            return new RegisterBookDTO
            {
                IdBook = newBook.Id,
                AuthorId = newBook.AuthorId,
                AuthorName = newBook.AuthorName,
                Title = newBook.Title,
                Abstract = newBook.Abstract,
                Quantity = newBook.Quantity

            };


        }

        public RegisterBookDTO Update(Guid idBook, RegisterBookDTO registerBookDTO)
        {
            var bookExist = _bookRepository.Get(idBook);
            if (bookExist == null)
                throw new Exception("O Livro não existe");


            var newBookUpdate = new Book
            {
                Id = idBook,
                Title = registerBookDTO.Title,
                Abstract = registerBookDTO.Abstract,
                Quantity = registerBookDTO.Quantity


            };

            _bookRepository.Update(idBook, newBookUpdate);

            return new RegisterBookDTO
            {
                IdBook = idBook,
                AuthorId = bookExist.AuthorId,
                AuthorName = bookExist.AuthorName,
                Title = newBookUpdate.Title,
                Abstract = newBookUpdate.Abstract,
                Quantity = newBookUpdate.Quantity

            };

        }

        public bool Delete(Guid id)
        {
            return _bookRepository.Remove(id);
        }



    }
}
