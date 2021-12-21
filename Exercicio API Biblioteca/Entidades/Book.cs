using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.Entidades
{
    public class Book : BaseEntity
    {
        
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public int Quantity { get; set; }
    }
}
