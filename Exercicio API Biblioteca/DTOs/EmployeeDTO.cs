using Exercicio_API_Biblioteca.Entidades;
using Exercicio_API_Biblioteca.Enums;
using System;

namespace Exercicio_API_Biblioteca.DTOs
{
    public class EmployeeDTO : Validator
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
      
       


        public override void Validar()
        {
            Valido = true;

            if (Name == null)
                Valido = false;

            if (CPF == null)
                Valido = false;

            if (Email == null)
                Valido = false;


        }
    }
}
