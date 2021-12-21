using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.Entidades
{
    public class Author : BaseEntity
    {
        public Author()
        {
            BooksList = new List<BookList>();
        }


        public string NameAuthor { get; set; }
        public string Biography { get; set; }
        public string Nationality { get; set; }
        public List<BookList>? BooksList { get; set; }

    }
}
