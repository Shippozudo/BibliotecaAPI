using System;

namespace Exercicio_API_Biblioteca.DTOs
{
    public class RegisterBookDTO
    {

        public Guid? IdBook { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public int Quantity { get; set; }

    }
}
