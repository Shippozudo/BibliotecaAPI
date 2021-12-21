using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Entidades;
using Exercicio_API_Biblioteca.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercicio_API_Biblioteca.Services
{
    public class EmployeeService
    {
        
        private readonly EmployeeRepository _employeeRepository;
        private readonly UserRepository _userRepository;
        private readonly List<Employee> _employee;
        private readonly UserClientService _userClientService;
        
        public EmployeeService(EmployeeRepository employeeRepository, UserRepository userRepository, UserClientService userClientService)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _employee ??= new List<Employee>();
           _userClientService = userClientService;
        }


        public IEnumerable<Employee> Get()
        {
            return _employeeRepository.Get();
        }

        public Employee Get(Guid id)
        {
            return _employeeRepository.Get(id);

        }


        public EmployeeDTO Create(CreateEmployeeDTO createEmployeeDTO)
        {
            var user = new User
            {
                Role = "Funcionário",
                Username = createEmployeeDTO.NewUserDTO.Username,
                Password = createEmployeeDTO.NewUserDTO.Password,

            };
            _userRepository.Create(user);

            var employee = new Employee
            {
                Id = user.Id,
                Name = createEmployeeDTO.EmployeeDTO.Name,
                CPF = createEmployeeDTO.EmployeeDTO.CPF,
                Email = createEmployeeDTO.EmployeeDTO.Email,
            };

            _employeeRepository.Create(employee);

            return new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                CPF = employee.CPF,
                Email = employee.Email
            };
        }

        public EmployeeDTO Update(Guid id, CreateEmployeeDTO createEmployeeDTO)
        {
            var user = new User
            {
                Role = "Funcionário",
                Username = createEmployeeDTO.NewUserDTO.Username,
                Password = createEmployeeDTO.NewUserDTO.Password,
            };

            _userRepository.Update(id, user);

            var employee = new Employee
            {
                Id = id,
                Name = createEmployeeDTO.EmployeeDTO.Name,
                CPF = createEmployeeDTO.EmployeeDTO.CPF,
                Email = createEmployeeDTO.EmployeeDTO.Email,
            };
            _employeeRepository.Update(id, employee);

            return new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                CPF = employee.CPF,
                Email = employee.Email
            };


        }


    }
}
