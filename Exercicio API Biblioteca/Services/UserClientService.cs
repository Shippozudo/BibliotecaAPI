using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Entidades;
using Exercicio_API_Biblioteca.Repositoy;
using System;
using System.Collections.Generic;
using static Exercicio_API_Biblioteca.DTOs.LoginResultDTO;

namespace Exercicio_API_Biblioteca.Services
{
    public class UserClientService
    {
        private readonly UserRepository _userRepository;
        private readonly JWTTokenService _tokenService;
        private readonly ClientRepository _clientRepository;
        private readonly EmployeeRepository _employeeRepository;
      

        public UserClientService(UserRepository userRepository,
                                 JWTTokenService tokenService,
                                 ClientRepository clientRepository,
                                 EmployeeRepository employeeRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
            
        }

     

        public UserDTO Create(User user)
        {
            var userExists = _userRepository.GetByUsername(user.Username);
            if (userExists != null)
                throw new Exception("O nome de usuário já esta sendo utilizado!!");

            var newUser = _userRepository.Create(user);

            return new UserDTO
            {
                Role = newUser.Role,
                Username = newUser.Username
            };

        }

        public UpdateUserDTO Update(Guid id, User user)
        {
            _userRepository.Update(id, user);

            return new UpdateUserDTO
            {
                Id = id,
                Role = user.Role,
                Username = user.Username,
                Password = user.Password

            };
            
        }

      
        public LoginResultDTO Login(string username, string password)
        {
            var loginResult = _userRepository.Login(username, password);
            

            if (loginResult.Error)
            {
                return new LoginResultDTO
                {
                    Success = false,
                    Errors = new string[] { $"Ocorreu um erro ao autenticar:{loginResult.Exception?.Message}" }

                };
            }
            var token = _tokenService.GenerateToken(loginResult.User);

           
            return new LoginResultDTO
            {
                Errors = null,
                Success = true,
                

                User = new UserLoginResultDTO
                {
                    Id = loginResult.User.Id,
                    Token = token,
                    Role = loginResult.User.Role,
                    Username = username
                }
            };
        }


        public IEnumerable<User> Get()
        {
            return _userRepository.Get();
        }

        public User Get(Guid id)
        {
            return _userRepository.Get(id);
        }
    }

}
