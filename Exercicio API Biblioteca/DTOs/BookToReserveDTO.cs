using Exercicio_API_Biblioteca.Entidades;
using Exercicio_API_Biblioteca.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
            var dateStart = DateTime.ParseExact(StartDateReserve, "dd/MM/yyyy", CultureInfo.CurrentCulture);
            var dateEnd = DateTime.ParseExact(EndDateReserve, "dd/MM/yyyy", CultureInfo.CurrentCulture);

            Valido = true;

            if (StartDateReserve == null)
                Valido = false;
            if (EndDateReserve == null)
                Valido = false;

            if(dateStart < DateTime.UtcNow)
            {
                Valido = false;
                throw new Exception("A data não pode ser anterior ao dia de hoje");

            }
            if (dateStart.AddDays(5) > dateEnd)
            {
                Valido = false;
                throw new Exception("O tempo de emprestimo deve ser de no mínimo 5 dias ");
            }
            if (BookListReserve.Any() != true)
            {   
                throw new Exception("A lista de livros para reservar está vazia");
            }
        }
    }
}
