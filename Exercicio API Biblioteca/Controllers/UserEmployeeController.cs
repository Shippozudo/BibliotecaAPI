using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Repositoy;
using Exercicio_API_Biblioteca.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exercicio_API_Biblioteca.Controllers
{

    [ApiController, Route("[controller]")]


    public class UserEmployeeController : ControllerBase
    {

        private readonly UserRepository _userRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly UserEmployeeService _userEmployeeService;
        private readonly EmployeeService _employeeService;
        private readonly UserClientService _userClientService;


        public UserEmployeeController(UserRepository userRepository,
                                      EmployeeRepository employeeRepository,
                                      UserEmployeeService userEmployeeService,
                                      EmployeeService employeeService,
                                      UserClientService userClientService)
        {
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
            _userEmployeeService = userEmployeeService;
            _employeeService = employeeService;
            _userClientService = userClientService;
        }

        [HttpGet, AllowAnonymous, Route("/users/employee")]
        public IActionResult Get()
        {
            return Ok(_employeeService.Get());
        }


        [HttpGet, AllowAnonymous, Route("/users/employee/{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_employeeService.Get(id));
        }



        [HttpPost, AllowAnonymous, Route("/users/employee")]
        public IActionResult Register([FromBody] CreateEmployeeDTO createEmployeeDTO)
        {
            return Created("", _employeeService.Create(createEmployeeDTO));
        }




        [HttpPost, AllowAnonymous, Route("/user/employee/login")]
        public IActionResult Login([FromBody] UserLoginDTO loginDTO)
        {
            return Ok(_userClientService.Login(loginDTO.Username, loginDTO.Password));
        }




        [HttpPut, AllowAnonymous, Route("/users/employee/{id}")]
        public IActionResult Update(Guid id, CreateEmployeeDTO createEmployeeDTO)
        {
            return Created("", _employeeService.Update(id, createEmployeeDTO));

        }
















    }


}
