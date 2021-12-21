using Exercicio_API_Biblioteca.Entidades;
using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.DTOs
{
    public class ClientDTO : Validator
    {


        public Guid? IdClient { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string? Email { get; set; }
        public string CEP { get; set; }
        public string? Birthdate { get; set; }
        public Adress? Adress { get; set; }
        public List<BookToReserveDTO> BookToReserveDTO { get; set; }



        public override void Validar()
        {
            Valido = true;

            if (Name == null)
                Valido = false;

            if (CPF == null)
                Valido = false;

            if (Email == null)
                Valido = false;

            if (CPF == null)
                Valido = false;

        }
    }
}
