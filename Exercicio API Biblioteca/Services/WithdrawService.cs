using Exercicio_API_Biblioteca.Entidades;
using Exercicio_API_Biblioteca.Extension;
using Exercicio_API_Biblioteca.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercicio_API_Biblioteca.Services
{
    public class WithdrawService
    {
        private readonly ClientRepository _clientRepository;
        private readonly BookRepository _bookRepository;
        private readonly WithdrawRepository _withdrawRepository;
        private readonly BookToReserveRepository _bookToReserveRepository;

        public WithdrawService(ClientRepository clientRepository,
                               BookRepository bookRepository,
                               WithdrawRepository withdrawRepository,
                               BookToReserveRepository bookToReserveRepository)
        {
            _clientRepository = clientRepository;
            _bookRepository = bookRepository;
            _withdrawRepository = withdrawRepository;
            _bookToReserveRepository = bookToReserveRepository;
        }



        public IEnumerable<Withdraw> Get(string startWithdraw, string endtWithdraw, string bookTitle, string author, int page, int items)
        {
            return _withdrawRepository.Get()
                .WhereIfIsNotNull(startWithdraw, b => b.StartWithdraw.ToString().Contains(startWithdraw, StringComparison.CurrentCultureIgnoreCase))
                .WhereIfIsNotNull(endtWithdraw, b => b.EndWithdraw.ToString().Contains(endtWithdraw, StringComparison.CurrentCultureIgnoreCase))
                .WhereIfIsNotNull(bookTitle, b => b.BookListReserve.Any(t => t.Title.Contains(bookTitle, StringComparison.CurrentCultureIgnoreCase)))
                .WhereIfIsNotNull(author, b => b.BookListReserve.Any(t => t.Author.Contains(author, StringComparison.CurrentCultureIgnoreCase)))
                .Skip(page == 0 ? 0 : ((page - 1) * page))
                .Take(items)
                .ToList();
            
        }

        public Withdraw Get(Guid idWithdraw)
        {
            return _withdrawRepository.Get(idWithdraw);
        }
    }
}
