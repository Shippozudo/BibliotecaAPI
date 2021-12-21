using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Entidades;
using Exercicio_API_Biblioteca.Repositoy;
using System;

namespace Exercicio_API_Biblioteca.Services
{
    public class UserEmployeeService
    {
        private readonly UserRepository _userRepository;
        private readonly JWTTokenService _tokenService;


        public UserEmployeeService(UserRepository userRepository, JWTTokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

     
    }
}
