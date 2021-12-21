using Exercicio_API_Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercicio_API_Biblioteca.Repositoy
{
    public class EmployeeRepository
    {

        private readonly Dictionary<Guid, Employee> _employees;

        public EmployeeRepository()
        {
            _employees ??= new Dictionary<Guid, Employee>();
        }

        public IEnumerable<Employee> Get()
        {
            return _employees.Values;
        }

        public Employee Get(Guid id)
        {
            if (_employees.TryGetValue(id, out var employee))
                return employee;

            throw new Exception("Funcionario não encontrado");
        }

        public Employee GetByUsername(string name)
        {
            return _employees.Values.Where(u => u.Name == name).FirstOrDefault();
        }

        public Employee Create(Employee employee)
        {
            if (_employees.TryAdd(employee.Id, employee))
                return employee;

            throw new Exception("Nao foi possivel cadastrar o funcionário");
        }

        public bool Remove(Guid id)
        {
            return _employees.Remove(id);
        }

        public Employee Update(Guid id, Employee employee)
        {
            if (_employees.TryGetValue(id, out var employeeToUpdate))
            {
                employeeToUpdate.Id = id;
                employeeToUpdate.Name = employee.Name;
                employeeToUpdate.CPF = employee.CPF;
                employeeToUpdate.Email = employee.Email;


                return Get(id);
            }
            throw new Exception("Funcionário nao encontrado");
        }

    }
}
