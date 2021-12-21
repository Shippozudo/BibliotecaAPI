using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Entidades;
using Exercicio_API_Biblioteca.Repositoy;
using Exercicio_API_Biblioteca.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Exercicio_API_Biblioteca.Controllers
{
    [ApiController, Route("[controller]")]
    public class UserClientController : ControllerBase
    {
        private readonly UserClientService _userClientService;
        private readonly UserRepository _userRepository;
        private readonly ClientService _clientService;
        private readonly ClientRepository _clientRepository;



        public UserClientController(UserClientService userClientService, UserRepository userRepository, ClientService clientService, ClientRepository clientRepository)
        {
            _userClientService = userClientService;
            _userRepository = userRepository;
            _clientService = clientService;
            _clientRepository = clientRepository;



        }


        [HttpGet, AllowAnonymous, Route("/users/client")]
        public IActionResult Get([FromQuery] string Name, string CPF, string birthdate, int Page, int Items)
        {
            return Ok(_clientService.Get(Name, CPF, birthdate, Page, Items));
        }


        [HttpGet, AllowAnonymous, Route("/users/client/{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_clientService.Get(id));

        }



        [HttpPost, AllowAnonymous, Route("/users/client")]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateClientDTO createClientDTO)
        {

            return Created("", await _clientService.CreateAsync(createClientDTO));

        }


        [HttpPost, AllowAnonymous, Route("/user/client/login")]
        public IActionResult Login([FromBody] UserLoginDTO loginDTO)
        {
            return Ok(_userClientService.Login(loginDTO.Username, loginDTO.Password));

        }

        [HttpPut, AllowAnonymous, Route("/users/client/{id}")]
        public IActionResult Update(Guid id, [FromBody] CreateClientDTO createClientDTO)
        {
            return Created(" ", _clientService.Update(id, createClientDTO));
        }



       






    }
}
