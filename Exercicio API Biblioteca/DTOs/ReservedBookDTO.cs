using Exercicio_API_Biblioteca.Entidades;
using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.DTOs
{
    public class ReservedBookDTO : Validator
    {
        public ReservedBookDTO()
        {
            BookListReserve = new List<BookListReserve>();
        }

        public Guid IdReserve { get; set; }
        public List<BookListReserve> BookListReserve { get; set; }
        public DateTime StartDateReserve { get; set; }
        public DateTime EndDateReserve { get; set; }
        public string Status { get; set; }

        public override void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
