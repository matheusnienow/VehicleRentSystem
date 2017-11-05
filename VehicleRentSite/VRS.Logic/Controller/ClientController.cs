using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRS.Model;
using VRS.Model.Repository;

namespace VRS.Logic.Controller
{
    public class ClientController
    {
        IRepository<Client> repo = Repository<Client>.NewInstance();

        public Client CreateClient(string name, string surname, char sex, string phone, string city, DateTime birthDate, int userId)
        {
            Client client = new Client(name, surname, sex.ToString(), phone, city, birthDate, userId);
            var result = repo.Insert(client);
            if (result > 0){
                return client;
            }
            return null;
        }
    }
}
