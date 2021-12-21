using Exercicio_API_Biblioteca.DTOs;
using System;
using System.Collections.Generic;


namespace Exercicio_API_Biblioteca.Entidades
{
    public class Client : Person
    {
        public Client()
        {
            ReservedBook = new List<ReservedBookDTO>();
            Withdraws = new List<FinishWithdrawDTO>();
        }

        public Guid Id { get; set; }
        public List<ReservedBookDTO>? ReservedBook { get; set; }
        public List<FinishWithdrawDTO>? Withdraws { get; set; }
        public string?  CEP { get; set; }
        public Adress? Adress { get; set; }



    }
}
