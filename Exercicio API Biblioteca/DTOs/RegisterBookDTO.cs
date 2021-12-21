using System;

namespace Exercicio_API_Biblioteca.DTOs
{
    public class RegisterBookDTO : Validator
    {

        public Guid? IdBook { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public int Quantity { get; set; }

        public override void Validar()
        {
            Valido = true;

            if (Quantity < 0)
            {
                Valido = false;
                throw new Exception("Quantidade não pode ser menor que 0");

            }
            if (Title == "" || Title == "string")
            {
                Valido = false;
                throw new Exception("Titulo não pode estar vazio");

            }


        }
    }
}
