using Exercicio_API_Biblioteca.Entidades;
using Exercicio_API_Biblioteca.Enums;
using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.DTOs
{
    public class BookToReserveDTO : Validator
    {
        public BookToReserveDTO()
        {
            BookListReserve = new List<BookListReserve>();
        }

        public Guid IdReserve { get; set; }
        public List<BookListReserve> BookListReserve { get; set; }
        public string StartDateReserve { get; set; }
        public string EndDateReserve { get; set; }
        public string Status { get; set; }

        public override void Validar()
        {
            Valido = true;

            if (StartDateReserve == null)
                Valido = false;
            if (EndDateReserve == null)
                Valido = false;
        }
    }
}
