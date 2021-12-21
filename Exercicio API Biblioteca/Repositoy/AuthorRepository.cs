using Exercicio_API_Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercicio_API_Biblioteca.Repositoy
{
    public class AuthorRepository
    {
        private readonly Dictionary<Guid, Author> _authors;

        public AuthorRepository()
        {
            _authors ??= new Dictionary<Guid, Author>();
        }

        public IEnumerable<Author> Get()
        {
            return _authors.Values;
        }

        public Author Get(Guid id)
        {
            if (_authors.TryGetValue(id, out var author))
                return author;

            throw new Exception("Autor não encontrado");
        }

        public Author GetByUsername(string name)
        {
            return _authors.Values.Where(u => u.NameAuthor == name).FirstOrDefault();
        }

        public Author Create(Author author)
        {


            if (_authors.TryAdd(author.Id, author))
                return author;

            throw new Exception("Nao foi possivel cadastrar o Autor");
        }

        

        public bool Remove(Guid id)
        {
            return _authors.Remove(id);
        }

        public Author Update(Guid id, Author author)
        {
            if (_authors.TryGetValue(id, out var authorToUpdate))
            {
                authorToUpdate.Id = id;
                authorToUpdate.NameAuthor = author.NameAuthor;
                authorToUpdate.Biography = author.Biography;
                authorToUpdate.Nationality = author.Nationality;
                

                return Get(id);
            }
            throw new Exception("Autor nao encontrado");
        }
    }
}
