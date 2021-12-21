using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Entidades;
using Exercicio_API_Biblioteca.Repositoy;
using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.Services
{
    public class BookToReserveService
    {
        private readonly BookToReserveRepository _bookToReserveRepository;
        private readonly ClientRepository _clientRepository;
        private readonly BookRepository _bookRepository;
        private readonly ClientService _clientService;
        private readonly BookService _bookService;


        public BookToReserveService(BookToReserveRepository bookToReserveRepository,
                                    ClientRepository clientRepository,
                                    BookRepository bookRepository,
                                    ClientService clientService,
                                    BookService bookService)


        {
            _bookToReserveRepository = bookToReserveRepository;
            _clientRepository = clientRepository;
            _bookRepository = bookRepository;
            _clientService = clientService;
            _bookService = bookService;

        }





        public IEnumerable<BookToReserve> Get()
        {
            return _bookToReserveRepository.Get();
        }

        public BookToReserve Get(Guid id)
        {
            return _bookToReserveRepository.Get(id);
        }





    }






}
