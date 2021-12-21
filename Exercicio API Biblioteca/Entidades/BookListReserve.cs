using System;

namespace Exercicio_API_Biblioteca.Entidades
{
    public class BookListReserve : BaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }


    }
}
