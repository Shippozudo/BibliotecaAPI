using Exercicio_API_Biblioteca.Entidades;
using System;
using System.Collections.Generic;

namespace Exercicio_API_Biblioteca.Repositoy
{
    public class WithdrawRepository
    {
        private readonly Dictionary<Guid, Withdraw> _withdraw;

        public WithdrawRepository()
        {
            _withdraw ??= new Dictionary<Guid, Withdraw>();
        }

        public IEnumerable<Withdraw> Get()
        {
            return _withdraw.Values;
        }

        public Withdraw Get(Guid id)
        {
            if (_withdraw.TryGetValue(id, out var withdraw))
                return withdraw;

            throw new Exception("Retirada não encontrado");
        }



        public Withdraw Create(Withdraw withdraw)
        {            
            if (_withdraw.TryAdd(withdraw.Id, withdraw))
                return withdraw;

            throw new Exception("Nao foi possivel cadastrar a Retirada");
        }



        public bool Remove(Guid id)
        {
            return _withdraw.Remove(id);
        }

        public Withdraw Update(Guid id, Withdraw withdraw)
        {
            if (_withdraw.TryGetValue(id, out var withdrawToUpdate))
            {
                withdrawToUpdate.Id = id;
                withdrawToUpdate.StartWithdraw = withdraw.StartWithdraw;
                withdrawToUpdate.EndWithdraw = withdraw.EndWithdraw;



                return Get(id);
            }
            throw new Exception("Retirada nao encontrado");
        }
    }
}
