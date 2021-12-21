using Exercicio_API_Biblioteca.Enums;
using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.Entidades
{
    public class BookToReserve : BaseEntity 
    {
        public BookToReserve()
        {
            BookListReserve = new List<BookListReserve>();
        }

        
        public List<BookListReserve> BookListReserve { get; set; }
        public DateTime StartDateReserve { get; set; }
        public DateTime EndDateReserve { get; set; }
        public string Status { get; set; }
       
      

    }
}
