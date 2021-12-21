using Exercicio_API_Biblioteca.Entidades;
using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.DTOs
{
    public class WithdrawDTO
    {
        public WithdrawDTO()
        {
            BookListReserve = new List<BookListReserve>();
        }

        public Guid Id { get; set; }
        public List<BookListReserve> BookListReserve { get; set; }
        public string StartWithdraw { get; set; }
        public string EndWithdraw { get; set; }
        public string Status { get; set; }
    }
}
