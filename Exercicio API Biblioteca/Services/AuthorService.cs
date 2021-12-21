using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Entidades;
using Exercicio_API_Biblioteca.Repositoy;
using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.Services
{
    public class AuthorService
    {
        private readonly AuthorRepository _authorRepository;

        public AuthorService(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }



        public IEnumerable<Author> Get()
        {
            return _authorRepository.Get();
        }

        public Author Get(Guid id)
        {
            return _authorRepository.Get(id);
        }


        public NewAuthorDTO Create(NewAuthorDTO newAuthorDTO)
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                NameAuthor = newAuthorDTO.NameAuthor,
                Biography = newAuthorDTO.Biography,
                Nationality = newAuthorDTO.Nationality

            };
            _authorRepository.Create(author);
            
            return new NewAuthorDTO
            {
                IdAuthor = author.Id,
                NameAuthor = author.NameAuthor,
                Biography = author.Biography,
                Nationality = author.Nationality
                
            };

        
        }



        public NewAuthorDTO Update(Guid id, NewAuthorDTO newAuthorDTO)
        {
            var author = new Author
            {
                Id = id,
                NameAuthor = newAuthorDTO.NameAuthor,
                Biography = newAuthorDTO.Biography,
                Nationality = newAuthorDTO.Nationality

            };
            _authorRepository.Update(id, author);

            return new NewAuthorDTO
            {
                IdAuthor = id,
                NameAuthor = author.NameAuthor,
                Biography = author.Biography,
                Nationality = author.Nationality
            };


        }

        public bool Delete(Guid id)
        {
            return _authorRepository.Remove(id);
             

        }






    }
}
