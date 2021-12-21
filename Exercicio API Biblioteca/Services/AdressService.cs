using Exercicio_API_Biblioteca.DTOs;
using Exercicio_API_Biblioteca.Entidades;
using Exercicio_API_Biblioteca.Repositoy;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exercicio_API_Biblioteca.Services
{
    
    public class AdressService
    {
        private readonly ClientRepository _clientRepository;



        public AdressService(ClientRepository clientRepository)

        {
            _clientRepository = clientRepository;



        }

        public async Task<Client> CreateAdressAsync(Client client)
        {
            var jsonOptions = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            var http = new HttpClient();
            

            var siteInicio = ("https://viacep.com.br/ws/");
            var siteFim = "/json/";
            var cliCEP = client.CEP;


            var siteTeste = await http.GetAsync(String.Format("{0}{1}{2}", siteInicio, cliCEP, siteFim));


            if (siteTeste.IsSuccessStatusCode)
            {
                var siteResult = await siteTeste.Content.ReadAsStringAsync();
                var deserial = JsonConvert.DeserializeObject<AdressDTO>(siteResult);


                var clientAdress = new Client
                {
                    Id = client.Id,
                    Name = client.Name,
                    CPF = client.CPF,
                    Email = client.Email,
                    CEP = client.CEP,
                    Adress = new Adress
                    {
                        CEP = deserial.CEP,
                        Street = deserial.Logradouro,
                        Complement = deserial.Complemento,
                        District = deserial.Bairro,
                        Location = deserial.Localidade,
                        State = deserial.UF

                    }


                };
                _clientRepository.Update(client.Id, clientAdress);
          
                return clientAdress;

            }
            else
            {
                 siteTeste.StatusCode = System.Net.HttpStatusCode.BadRequest;
                throw new Exception("CEP invalido ou não localizado.");
            }


            



        }
    }
}
