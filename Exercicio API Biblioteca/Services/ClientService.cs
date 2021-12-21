using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Entidades;
using Exercicio_API_Biblioteca.Enums;
using Exercicio_API_Biblioteca.Extension;
using Exercicio_API_Biblioteca.Repositoy;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio_API_Biblioteca.Services
{
    public class ClientService
    {
        private readonly List<Client> _client;
        private readonly ClientRepository _clientRepository;
        private readonly UserRepository _userRepository;
        private readonly BookRepository _bookRepository;
        private readonly BookToReserveRepository _bookToReserveRepository;
        private readonly WithdrawRepository _withdrawRepository;
        private readonly AuthorRepository _authorRepository;
        private readonly AdressService _adressService;



        public ClientService(ClientRepository clientRepository,
                             UserRepository userRepository,
                             BookRepository bookRepository,
                             BookToReserveRepository bookToReserveRepository,
                             WithdrawRepository withdrawRepository,
                             AuthorRepository authorRepository,
                             AdressService adressService)
        {
            _client = new List<Client>();
            _userRepository = userRepository;
            _clientRepository = clientRepository;
            _bookRepository = bookRepository;
            _bookToReserveRepository = bookToReserveRepository;
            _withdrawRepository = withdrawRepository;
            _authorRepository = authorRepository;
            _adressService = adressService;
        }


        public async Task<ClientDTO> CreateAsync(CreateClientDTO createClientDTO)
        {

            var user = new User
            {
                Role = "Cliente",
                Username = createClientDTO.NewUserDTO.Username,
                Password = createClientDTO.NewUserDTO.Password,


            };
            _userRepository.Create(user);

            var client = new Client
            {
                Id = user.Id,
                Name = createClientDTO.ClientDTO.Name,
                CPF = createClientDTO.ClientDTO.CPF,
                Email = createClientDTO.ClientDTO.Email,
                CEP = createClientDTO.ClientDTO.CEP,
                Adress = createClientDTO.ClientDTO.Adress,
                Birthdate = createClientDTO.ClientDTO.Birthdate

            };
            _clientRepository.Create(client);

            var adress = await _adressService.CreateAdressAsync(client);


            return new ClientDTO
            {
                IdClient = client.Id,
                Name = client.Name,
                CPF = client.CPF,
                Email = client.Email,
                CEP = client.CEP,
                Adress = adress.Adress,
                Birthdate = client.Birthdate


            };


        }


        public ClientDTO Update(Guid id, CreateClientDTO createClientDTO)
        {

            var user = new User
            {
                Role = "Cliente",
                Username = createClientDTO.NewUserDTO.Username,
                Password = createClientDTO.NewUserDTO.Password,


            };
            _userRepository.Update(id, user);

            var client = new Client
            {
                Id = id,
                Name = createClientDTO.ClientDTO.Name,
                CPF = createClientDTO.ClientDTO.CPF,
                Email = createClientDTO.ClientDTO.Email

            };

            _clientRepository.Update(id, client);

            return new ClientDTO
            {
                IdClient = client.Id,
                Name = client.Name,
                CPF = client.CPF,
                Email = client.Email

            };
        }


        public IEnumerable<Client> Get(string name, string cPF, string birthdate,  int page, int items)
        {
            return _clientRepository.Get()
                .WhereIfIsNotNull(name, b => b.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                .WhereIfIsNotNull(cPF, b => b.CPF.Contains(cPF, StringComparison.CurrentCultureIgnoreCase))
                .WhereIfIsNotNull(birthdate, b => b.Birthdate.Contains(birthdate, StringComparison.CurrentCultureIgnoreCase))
                .Skip(page == 0 ? 0 : ((page - 1) * page))
                .Take(items)
                .ToList();

        }


        public Client Get(Guid id)
        {
            return _clientRepository.Get(id);
        }


        public Client GetAll(Guid id)
        {
            return _clientRepository.Get(id);
        }


        public ReservedBookDTO Reserve(Guid idClient, BookToReserveDTO bookToReserveDTO)
        {
            bookToReserveDTO.Validar();


            var cli = _clientRepository.Get(idClient);

            // Parse Date
            var dateStart = DateTime.ParseExact(bookToReserveDTO.StartDateReserve, "dd/MM/yyyy", CultureInfo.CurrentCulture);
            var dateEnd = DateTime.ParseExact(bookToReserveDTO.EndDateReserve, "dd/MM/yyyy", CultureInfo.CurrentCulture);

            var listBook = new List<BookListReserve>();
            var disponible = 0;

            foreach (var b in bookToReserveDTO.BookListReserve)
            {
                var book = _bookRepository.Get(b.Id);
                var bookAuthor = book.AuthorName;
                var bookTitle = book.Title;

                var disponibleBook = _bookToReserveRepository.Get()
               .Where(r => r.BookListReserve.Any(l => l.Id == b.Id))
               .Where(r => r.StartDateReserve <= dateStart)
               .Where(r => r.EndDateReserve >= dateEnd)
               .Where(r => r.Status == "Pending").ToList();


                disponible = disponibleBook.Count;

                if ((disponible + 1) > book.Quantity)
                {
                    throw new Exception("Quantidade indisponivel");
                }

                b.Author = bookAuthor;
                b.Title = bookTitle;

                listBook.Add(b);

            }


            var reserve = new BookToReserve
            {
                Id = Guid.NewGuid(),
                StartDateReserve = dateStart,
                EndDateReserve = dateEnd,
                Status = Estatus.Pending.ToString()

            };

            reserve.BookListReserve.AddRange(listBook); // adiciona lista de livros a  lista de livros da Reserva
            _bookToReserveRepository.Create(reserve); // cria a reserva

            var cliReserve = new ReservedBookDTO
            {
                IdReserve = reserve.Id,
                BookListReserve = reserve.BookListReserve,
                StartDateReserve = dateStart,
                EndDateReserve = dateEnd,
                Status = Estatus.Pending.ToString()

            };
            cli.ReservedBook.Add(cliReserve);

            return cliReserve;


        }


        public ReservedBookDTO UpdateReserve(Guid idReserve, Guid idClient, BookToReserveDTO bookToReserveDTO)
        {
            bookToReserveDTO.Validar();

            var reserve = _bookToReserveRepository.Get(idReserve);
            var cli = _clientRepository.Get(idClient);
            var dateStart = DateTime.ParseExact(bookToReserveDTO.StartDateReserve, "d/M/yyyy", CultureInfo.CurrentCulture);
            var dateEnd = DateTime.ParseExact(bookToReserveDTO.EndDateReserve, "d/M/yyyy", CultureInfo.CurrentCulture);

            if (dateStart.AddDays(5) > dateEnd)
            {
                dateEnd = dateStart.AddDays(5);
            }

            var listBook = new List<BookListReserve>();
            var disponible = 0;

            foreach (var b in reserve.BookListReserve)
            {
                var book = _bookRepository.Get(b.Id);

                var disponibleBook = _bookToReserveRepository.Get()
               .Where(r => r.BookListReserve.Any(l => l.Id == b.Id))
               .Where(r => r.StartDateReserve <= dateStart)
               .Where(r => r.EndDateReserve >= dateEnd)
               .Where(r => r.Status == "Pending").ToList();


                disponible = disponibleBook.Count;

                if ((disponible + 1) > book.Quantity)
                {
                    throw new Exception("Quantidade indisponivel");
                }

                listBook.Add(b);
            }


            var updateReserve = new BookToReserve
            {
                Id = reserve.Id,
                StartDateReserve = dateStart,
                EndDateReserve = dateEnd,
                Status = Estatus.Pending.ToString()

            };

            _bookToReserveRepository.Update(idReserve, updateReserve);

            var cliReserve = new ReservedBookDTO
            {
                IdReserve = updateReserve.Id,
                StartDateReserve = updateReserve.StartDateReserve,
                EndDateReserve = updateReserve.EndDateReserve,
                Status = Estatus.Pending.ToString()

            };
            cli.ReservedBook.RemoveAll(x => x.IdReserve == idReserve);
            cliReserve.BookListReserve.AddRange(listBook);
            cli.ReservedBook.Add(cliReserve);

            return cliReserve;



        }


        public ReservedBookDTO Cancel(Guid idReserve, Guid idClient)
        {
            var reserve = _bookToReserveRepository.Get(idReserve);
            var cancelDay = DateTime.Now;
            var cli = _clientRepository.Get(idClient);

            if (reserve.Status == "Pending")
            {
                if (cancelDay < reserve.StartDateReserve.AddDays(-1))
                {
                    if (cancelDay.DayOfWeek != DayOfWeek.Saturday && cancelDay.DayOfWeek != DayOfWeek.Sunday)
                    {
                        var updateReserve = new BookToReserve
                        {
                            Id = reserve.Id,
                            StartDateReserve = reserve.StartDateReserve,
                            EndDateReserve = reserve.EndDateReserve,
                            Status = Estatus.Canceled.ToString()

                        };

                        _bookToReserveRepository.Update(idReserve, updateReserve);

                        var cliReserve = new ReservedBookDTO
                        {
                            IdReserve = updateReserve.Id,
                            StartDateReserve = updateReserve.StartDateReserve,
                            EndDateReserve = updateReserve.EndDateReserve,
                            Status = Estatus.Canceled.ToString()


                        };

                        cli.ReservedBook.RemoveAll(x => x.IdReserve == idReserve);
                        cliReserve.BookListReserve.AddRange(updateReserve.BookListReserve);
                        cli.ReservedBook.Add(cliReserve);

                        return cliReserve;
                    }
                    else
                        throw new Exception("Você só pode cancelar uma reserva em dias úteis, em no máximo 24horas antes");
                }
                else
                    throw new Exception("Você só pode cancelar uma reserva no máximo 24horas antes");
            }
            else
                throw new Exception("Só é possível cancelar reservas que estão Pendentes");
        }


        public ReservedBookDTO Finalize(Guid idReserve, Guid idClient, BookToReserveDTO bookToReserveDTO)
        {
            var reserve = _bookToReserveRepository.Get(idReserve);

            if (reserve.Status == "Pending")
            {
                var updateReserve = new BookToReserve
                {
                    Id = reserve.Id,
                    StartDateReserve = reserve.StartDateReserve,
                    EndDateReserve = reserve.EndDateReserve,
                    Status = Estatus.Finalized.ToString()

                };

                _bookToReserveRepository.Update(idReserve, updateReserve);

                var cliReserve = new ReservedBookDTO
                {
                    IdReserve = updateReserve.Id,
                    StartDateReserve = updateReserve.StartDateReserve,
                    EndDateReserve = updateReserve.EndDateReserve,
                    Status = Estatus.Finalized.ToString()


                };

                var cli = _clientRepository.Get(idClient);
                cli.ReservedBook.RemoveAll(x => x.IdReserve == idReserve);
                cliReserve.BookListReserve.AddRange(updateReserve.BookListReserve);
                cli.ReservedBook.Add(cliReserve);

                return cliReserve;
            }
            else
                throw new Exception("Só é possível finalizar uma reserva Pendente");

        }


        public FinishWithdrawDTO RegisterWithdrawReserve(Guid idReserve, Guid idClient, WithdrawReserveDTO withdrawReserveDTO)
        {
            var reserve = _bookToReserveRepository.Get(idReserve);
            var cli = _clientRepository.Get(idClient);


            if (reserve.Status == "Pending")
            {
                var withdraw = new Withdraw
                {
                    Id = Guid.NewGuid(),
                    StartWithdraw = reserve.StartDateReserve,
                    EndWithdraw = reserve.EndDateReserve,
                    BookListReserve = reserve.BookListReserve,
                    Status = Estatus.Borrowed.ToString(),
                    IdReserve = idReserve

                };
                _withdrawRepository.Create(withdraw);


                var updateReserveToFinalize = new BookToReserve
                {
                    Id = reserve.Id,
                    StartDateReserve = reserve.StartDateReserve,
                    EndDateReserve = reserve.EndDateReserve,
                    Status = Estatus.Borrowed.ToString()

                };
                _bookToReserveRepository.Update(idReserve, updateReserveToFinalize);


                var cliUpdateReserveToFinalize = new ReservedBookDTO
                {
                    IdReserve = updateReserveToFinalize.Id,
                    StartDateReserve = updateReserveToFinalize.StartDateReserve,
                    EndDateReserve = updateReserveToFinalize.EndDateReserve,
                    Status = updateReserveToFinalize.Status

                };
                cli.ReservedBook.RemoveAll(x => x.IdReserve == idReserve);
                cliUpdateReserveToFinalize.BookListReserve.AddRange(reserve.BookListReserve);
                cli.ReservedBook.Add(cliUpdateReserveToFinalize);


                var cliWithdraw = new FinishWithdrawDTO
                {
                    IdWithdraw = withdraw.Id,
                    StartWithdraw = withdraw.StartWithdraw,
                    EndWithdraw = withdraw.EndWithdraw,
                    BookListReserve = withdraw.BookListReserve,
                    Status = withdraw.Status,
                    IdReserve = withdraw.IdReserve,
                };


                foreach (var b in reserve.BookListReserve)
                {
                    var idBook = b.Id;
                    var book = _bookRepository.Get(idBook);
                    book.Quantity--;
                }

                cli.Withdraws.Add(cliWithdraw);

                return cliWithdraw;
            }
            else
                throw new Exception("Já foi feita a retirada dessa reserva ");
        }


        public FinishWithdrawDTO RegisterWithdraw(Guid idClient, WithdrawDTO withdrawDTO)
        {
            var cli = _clientRepository.Get(idClient);
            var dateStart = DateTime.ParseExact(withdrawDTO.StartWithdraw, "dd/MM/yyyy", CultureInfo.CurrentCulture);
            var dateEnd = DateTime.ParseExact(withdrawDTO.EndWithdraw, "dd/MM/yyyy", CultureInfo.CurrentCulture);


            var listBook = new List<BookListReserve>();
            var disponible = 0;

            foreach (var b in withdrawDTO.BookListReserve)
            {
                var book = _bookRepository.Get(b.Id);
                var bookAuthor = book.AuthorName;
                var bookTitle = book.Title;

                var disponibleBook = _bookToReserveRepository.Get()
               .Where(r => r.BookListReserve.Any(l => l.Id == b.Id))
               .Where(r => r.StartDateReserve <= dateStart)
               .Where(r => r.EndDateReserve >= dateEnd)
               .Where(r => r.Status == "Pending").ToList();

                var startReserve = _bookToReserveRepository.Get()
               .Where(r => r.BookListReserve.Any(l => l.Id == b.Id))
               .Where(r => r.StartDateReserve < dateEnd)
               .Where(r => r.Status == "Pending").ToList();


                disponible = disponibleBook.Count;

                if ((disponible + 1) > book.Quantity)
                    throw new Exception("Quantidade indisponivel");
                if (startReserve.Count > book.Quantity)
                    throw new Exception("Quantidade indisponivel. Selecione uma data de entrega anterior a inserida");

                b.Title = bookTitle;
                b.Author = bookAuthor;
                listBook.Add(b);
            }

            var withdraw = new Withdraw
            {
                Id = Guid.NewGuid(),
                StartWithdraw = dateStart,
                EndWithdraw = dateEnd,
                Status = Estatus.Borrowed.ToString()

            };
            withdraw.BookListReserve.AddRange(listBook);
            _withdrawRepository.Create(withdraw);

            var cliWithdraw = new FinishWithdrawDTO
            {
                IdWithdraw = withdraw.Id,
                BookListReserve = withdraw.BookListReserve,
                StartWithdraw = withdraw.StartWithdraw,
                EndWithdraw = withdraw.EndWithdraw,
                Status = withdraw.Status
            };

            foreach (var b in cliWithdraw.BookListReserve)
            {
                var idBook = b.Id;
                var book = _bookRepository.Get(idBook);
                book.Quantity--;
            }

            cli.Withdraws.Add(cliWithdraw);
            return cliWithdraw;




        }


        public FinishWithdrawDTO FinalizeWithdrawReserve(Guid idReserve, Guid idClient, Guid idWithdraw)
        {
            var withdrawGet = _withdrawRepository.Get(idWithdraw);
            var reserve = _bookToReserveRepository.Get(idReserve);
            var cli = _clientRepository.Get(idClient);

            if (reserve.Status == "Borrowed" && withdrawGet.Status == "Borrowed")
            {
                var withdraw = new Withdraw
                {
                    Id = idWithdraw,
                    StartWithdraw = reserve.StartDateReserve,
                    EndWithdraw = reserve.EndDateReserve,
                    BookListReserve = reserve.BookListReserve,
                    Status = Estatus.Finalized.ToString()

                };
                _withdrawRepository.Update(idWithdraw, withdraw);

                var updateReserveToFinalize = new BookToReserve
                {
                    Id = reserve.Id,
                    StartDateReserve = reserve.StartDateReserve,
                    EndDateReserve = reserve.EndDateReserve,
                    Status = Estatus.Finalized.ToString()

                };
                _bookToReserveRepository.Update(idReserve, updateReserveToFinalize);

                var cliUpdateReserveToFinalize = new ReservedBookDTO
                {
                    IdReserve = updateReserveToFinalize.Id,
                    StartDateReserve = updateReserveToFinalize.StartDateReserve,
                    EndDateReserve = updateReserveToFinalize.EndDateReserve,
                    Status = updateReserveToFinalize.Status

                };
                cli.ReservedBook.RemoveAll(x => x.IdReserve == idReserve);
                cliUpdateReserveToFinalize.BookListReserve.AddRange(reserve.BookListReserve);
                cli.ReservedBook.Add(cliUpdateReserveToFinalize);


                var cliWithdraw = new FinishWithdrawDTO
                {
                    IdWithdraw = withdraw.Id,
                    StartWithdraw = withdraw.StartWithdraw,
                    EndWithdraw = withdraw.EndWithdraw,
                    BookListReserve = withdraw.BookListReserve,
                    Status = withdraw.Status
                };


                foreach (var b in reserve.BookListReserve)
                {
                    var idBook = b.Id;
                    var book = _bookRepository.Get(idBook);
                    book.Quantity++;
                }

                cli.Withdraws.Add(cliWithdraw);

                return cliWithdraw;
            }
            else
                throw new Exception("Já foi feito a devolução desse Emprestimo ");
        }


        public FinishWithdrawDTO FinalizeWithdraw(Guid idClient, Guid idWithdraw)
        {
            var withdrawGet = _withdrawRepository.Get(idWithdraw);
            var cli = _clientRepository.Get(idClient);

            if (withdrawGet.Status == "Borrowed")
            {
                var withdraw = new Withdraw
                {
                    Id = idWithdraw,
                    StartWithdraw = withdrawGet.StartWithdraw,
                    EndWithdraw = withdrawGet.EndWithdraw,
                    BookListReserve = withdrawGet.BookListReserve,
                    Status = Estatus.Finalized.ToString()

                };
                _withdrawRepository.Update(idWithdraw, withdraw);
                               
                var cliWithdraw = new FinishWithdrawDTO
                {
                    IdWithdraw = withdraw.Id,
                    StartWithdraw = withdraw.StartWithdraw,
                    EndWithdraw = withdraw.EndWithdraw,
                    BookListReserve = withdraw.BookListReserve,
                    Status = withdraw.Status
                };

                foreach (var b in cliWithdraw.BookListReserve)
                {
                    var idBook = b.Id;
                    var book = _bookRepository.Get(idBook);
                    book.Quantity++;
                }

                cli.Withdraws.Add(cliWithdraw);

                return cliWithdraw;
            }
            else
                throw new Exception("Já foi feito a devolução desse Emprestimo ");
        }

    }
}
