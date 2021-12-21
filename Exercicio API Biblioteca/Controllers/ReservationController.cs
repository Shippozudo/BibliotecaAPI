
using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Repositoy;
using Exercicio_API_Biblioteca.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exercicio_API_Biblioteca.Controllers
{
    [ApiController, Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ClientRepository _clientRepository;
        private readonly ClientService _clientService;
        private readonly BookToReserveRepository _bookToReserveRepository;
        private readonly BookToReserveService _bookToReserveService;

        public ReservationController(ClientRepository clientRepository,
                                     ClientService clientService,
                                     BookToReserveRepository bookToReserveRepository,
                                     BookToReserveService bookToReserveService)
        {
            _clientRepository = clientRepository;
            _clientService = clientService;
            _bookToReserveRepository = bookToReserveRepository;
            _bookToReserveService = bookToReserveService;
        }




        [HttpGet, Route("/reservations")] // TODAS RESERVAS
        public IActionResult Get()
        {
            return Ok(_bookToReserveService.Get());
        }

        
        
        [HttpGet, AllowAnonymous, Route("/reservations/{idReserve}")] //Reserva pelo ID
        public IActionResult Get(Guid idReserve)
        {
            return Ok(_bookToReserveService.Get(idReserve));
        }

        
        
        [HttpGet, AllowAnonymous, Route("/reservations/{idClient}")] //Todas reservas do Client
        public IActionResult GetAll(Guid idClient)
        {

            return Ok(_clientService.Get(idClient));

        }



        [HttpPost, AllowAnonymous, Route("/reservations/{idClient}")]
        public IActionResult Reserve(Guid idClient, [FromBody] BookToReserveDTO bookToReserveDTO)
        {
            return Created("", _clientService.Reserve(idClient, bookToReserveDTO));
        }



        [HttpPut, AllowAnonymous, Route("/reservarions/update/{idReserve}/{idClient}")]
        public IActionResult UpdateReserve(Guid idReserve, Guid idClient, [FromBody] BookToReserveDTO reservedBook)
        {
            return Ok(_clientService.UpdateReserve(idReserve, idClient, reservedBook));
        }



        [HttpPut, AllowAnonymous, Route("/reservations/cancel/{idReserve}/{idClient}")]
        public IActionResult Cancel(Guid idReserve, Guid idClient)
        {
            return Ok(_clientService.Cancel(idReserve, idClient));
        }



        [HttpPut, AllowAnonymous, Route("/reservations/finalize/{idReserve}/{idClient}")]
        public IActionResult Finalize(Guid idReserve, Guid idClient, BookToReserveDTO reservedBook)
        {
            return Ok(_clientService.Finalize(idReserve, idClient, reservedBook));
        }



    }
}
