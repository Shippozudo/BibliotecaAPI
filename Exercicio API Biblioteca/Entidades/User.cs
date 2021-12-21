using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Enums;
using System;

namespace Exercicio_API_Biblioteca.Entidades
{
    public class User : BaseEntity
    {

        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        


        public int FailedAttempts { get; set; }
        public bool IsLockout { get; set; }
        public DateTime? LockoutDate { get; set; }



    }
}
