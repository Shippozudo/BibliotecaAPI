using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.Entidades
{
    public class WithdrawReserve : BaseEntity
    {
        public WithdrawReserve()
        {
            BookListReserve = new List<BookListReserve>();
        }


        public List<BookListReserve> BookListReserve { get; set; }
        public DateTime StartWithdraw { get; set; }
        public DateTime EndWithdraw { get; set; }
        public string Status { get; set; }
        public Guid IdReserve { get; set; }

    }
}
