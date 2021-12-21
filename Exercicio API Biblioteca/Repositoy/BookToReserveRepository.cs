using Exercicio_API_Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercicio_API_Biblioteca.Repositoy
{
    public class BookToReserveRepository
    {
        private readonly Dictionary<Guid, BookToReserve> _bookToReserve;
        private readonly Dictionary<Guid, BookListReserve> _booklistReserve;

        public BookToReserveRepository()
        {
            _bookToReserve ??= new Dictionary<Guid, BookToReserve>();
            _booklistReserve = new Dictionary<Guid, BookListReserve>();
        }

        public IEnumerable<BookToReserve> Get()
        {
            return _bookToReserve.Values;
        }

        public BookToReserve Get(Guid id)
        {
            if (_bookToReserve.TryGetValue(id, out var reserve))
                return reserve;

            throw new Exception("Reserva não encontrado");
        }

       
        public BookListReserve AddBook(BookListReserve bookListReserve)
        {
            if (_booklistReserve.TryAdd(bookListReserve.Id, bookListReserve))
                return bookListReserve;
                       
            throw new Exception("Não foi possivel cadastrar o Livro na lista de reserva, talvez o livro já tenha sido cadastrado");
        }

        public BookToReserve Create(BookToReserve bookListReserve)
        {
            if (_bookToReserve.TryAdd(bookListReserve.Id, bookListReserve))
                return bookListReserve;

            throw new Exception("Nao foi possivel cadastrar a Reserva");
        }



        public bool Remove(Guid id)
        {
            return _bookToReserve.Remove(id);
        }

        public BookToReserve Update(Guid id, BookToReserve reserve)
        {
            if (_bookToReserve.TryGetValue(id, out var reserveToUpdate))
            {
                reserveToUpdate.Id = id;
                reserveToUpdate.StartDateReserve = reserve.StartDateReserve;
                reserveToUpdate.EndDateReserve = reserve.EndDateReserve;
                reserveToUpdate.Status = reserve.Status;



                return Get(id);
            }
            throw new Exception("Reserva nao encontrado");
        }
    }
}
