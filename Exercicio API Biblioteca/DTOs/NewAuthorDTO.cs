using Exercicio_API_Biblioteca.Entidades;
using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.DTOs
{
    public class NewAuthorDTO
    {

        public Guid? IdAuthor { get; set; }
        public string NameAuthor { get; set; }
        public string Biography { get; set; }
        public string Nationality { get; set; }
       

    }
}
