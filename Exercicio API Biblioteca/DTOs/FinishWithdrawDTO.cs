using Exercicio_API_Biblioteca.Entidades;
using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.DTOs
{
    public class FinishWithdrawDTO : Validator
    {
        public FinishWithdrawDTO()
        {
            BookListReserve = new List<BookListReserve>();
        }

        public Guid IdWithdraw { get; set; }
        public List<BookListReserve> BookListReserve { get; set; }
        public DateTime StartWithdraw { get; set; }
        public DateTime EndWithdraw { get; set; }
        public string Status { get; set; }
        public Guid? IdReserve { get; set; }

        public override void Validar()
        {
            Valido = true;

            if(StartWithdraw == null)
                Valido = false;
            if (EndWithdraw > StartWithdraw.AddDays(20))
            {
                Valido = false;
            }
           


        }
    }
}
