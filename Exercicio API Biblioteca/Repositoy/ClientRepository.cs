using Exercicio_API_Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercicio_API_Biblioteca.Repositoy
{
    public class ClientRepository
    {
        private readonly Dictionary<Guid, Client> _clients;

        public ClientRepository()
        {
            _clients ??= new Dictionary<Guid, Client>();
        }

        public IEnumerable<Client> Get()
        {
            return _clients.Values;
        }

        public Client Get(Guid id)
        {
            if (_clients.TryGetValue(id, out var client))
                return client;

            throw new Exception("Cliente não encontrado");
        }

        public Client GetByUsername(string name)
        {
            return _clients.Values.Where(u => u.Name == name).FirstOrDefault();
        }

        public Client Create(Client client)
        {
            

            if (_clients.TryAdd(client.Id, client))
                return client;

            throw new Exception("Nao foi possivel cadastrar o Cliente");
        }

        public bool Remove(Guid id)
        {
            return _clients.Remove(id);
        }

        public Client Update(Guid id, Client client)
        {
            if (_clients.TryGetValue(id, out var clientToUpdate))
            {
                clientToUpdate.Id = id;
                clientToUpdate.Name = client.Name;
                clientToUpdate.CPF = client.CPF;
                clientToUpdate.Email = client.Email;
                clientToUpdate.Adress = client.Adress;



                return Get(id);
            }
            throw new Exception("Cliente nao encontrado");
        }
    }
}
