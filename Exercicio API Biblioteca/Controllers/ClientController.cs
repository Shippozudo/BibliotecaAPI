using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Entidades;
using Exercicio_API_Biblioteca.Repositoy;
using Exercicio_API_Biblioteca.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exercicio_API_Biblioteca.Controllers
{

    [ApiController, Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;
        

        public ClientController(ClientService clientService, UserRepository userRepository)
        {
            _clientService = clientService;
            
        }

                

        

    }
}
