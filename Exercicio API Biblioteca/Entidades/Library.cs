using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.Entidades
{
    public class Libery : BaseEntity
    {
        public Libery(Guid id,
                      List<Author> authors,
                      List<Book> books,
                      List<Book> rentedBooks) 
        {
            Authors = authors;
            Books = books;
            RentedBooks = rentedBooks;
        }

        public List<Author>? Authors { get; set; }
        public List<Book>? Books { get; set; }
        public List<Book>? RentedBooks { get; set; }
    }
}
