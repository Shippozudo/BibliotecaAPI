﻿using System;

namespace Exercicio_API_Biblioteca.DTOs
{
    public class UpdateUserDTO
    {

        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
