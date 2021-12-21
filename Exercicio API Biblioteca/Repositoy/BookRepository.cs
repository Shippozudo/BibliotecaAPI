using Exercicio_API_Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercicio_API_Biblioteca.Repositoy
{
    public class BookRepository
    {
        private readonly Dictionary<Guid, Book> _books;

        public BookRepository()
        {
            _books ??= new Dictionary<Guid, Book>();
        }

        public IEnumerable<Book> Get()
        {
            return _books.Values;
        }

        public Book Get(Guid id)
        {
            if (_books.TryGetValue(id, out var book))
                return book;

            throw new Exception("Livro não encontrado");
        }

        public Book GetByUsername(string title)
        {
            return _books.Values.Where(u => u.Title == title).FirstOrDefault();
        }

        public Book Create(Book book)
        {

            book.Id = Guid.NewGuid();
            if (_books.TryAdd(book.Id, book))
                return book;

            throw new Exception("Nao foi possivel cadastrar o Livro");
        }

        public bool Remove(Guid id)
        {
            return _books.Remove(id);
        }

        public Book Update(Guid id, Book book)
        {
            if (_books.TryGetValue(id, out var bookToUpdate))
            {
                bookToUpdate.Id = id;
                bookToUpdate.Title = book.Title;
                bookToUpdate.Quantity = book.Quantity;


                return Get(id);
            }
            throw new Exception("Livro nao encontrado");
        }
    }
}
