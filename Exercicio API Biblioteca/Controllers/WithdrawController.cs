using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exercicio_API_Biblioteca.Controllers
{

    [ApiController, Route("[controller]")]

    public class WithdrawController : ControllerBase
    {

        private readonly ClientService _clientService;
        private readonly WithdrawService _withdrawService;

        public WithdrawController(ClientService clientService,
                                  WithdrawService withdrawService)
        {
            _clientService = clientService;
            _withdrawService = withdrawService;
        }


        //Authorize(Roles = "Funcionario")

        [HttpGet, AllowAnonymous, Route("/withdraw")] // TODAS RESERVAS
        public IActionResult Get([FromQuery] string startWithdraw, string endtWithdraw, string bookTitle, string author, int page, int items)
        {
            return Ok(_withdrawService.Get(startWithdraw, endtWithdraw, bookTitle, author, page, items));
        }


        [HttpGet, AllowAnonymous, Route("/withdraw/{idWithdraw}")] //Reserva pelo ID
        public IActionResult Get(Guid idWithdraw)
        {
            return Ok(_withdrawService.Get(idWithdraw));
        }



        [HttpPost, AllowAnonymous, Route("/withdraw/{idReserve}/{idClient}")]
        public IActionResult RegisterWithdrawReserve(Guid idReserve, Guid idClient, WithdrawReserveDTO withdrawReserveDTO)  // retira o livro e encerra a reserva
        {
            return Created("", _clientService.RegisterWithdrawReserve(idReserve, idClient, withdrawReserveDTO));
        }


        [HttpPost, AllowAnonymous, Route("/withdraw/{idClient}")]
        public IActionResult RegisterWithdraw(Guid idClient, [FromBody] WithdrawDTO withdrawDTO)
        {
            return Created("", _clientService.RegisterWithdraw(idClient, withdrawDTO));
        }

        [HttpPost, AllowAnonymous, Route("/withdraw/{idReserve}/{idClient}/{idWithdraw}")]
        public IActionResult FinalizeWithdrawReserve(Guid idReserve, Guid idClient, Guid idWithdraw)
        {
            return Created("", _clientService.FinalizeWithdrawReserve(idReserve, idClient, idWithdraw));
        }

        [HttpPost, AllowAnonymous, Route("/withdraw/{idClient}/{idWithdraw}")]
        public IActionResult FinalizeWithdraw(Guid idClient, Guid idWithdraw)
        {
            return Created("", _clientService.FinalizeWithdraw(idClient, idWithdraw));
        }



    }


}
