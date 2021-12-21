using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Enums;
using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.Entidades
{
    public class Withdraw :  BaseEntity
    {
        public Withdraw()
        {
            BookListReserve = new List<BookListReserve>();
        }

        
        public List<BookListReserve> BookListReserve { get; set; }
        public DateTime StartWithdraw { get; set; }
        public DateTime EndWithdraw { get; set; }
        public string Status { get; set; }
       
    }
}
