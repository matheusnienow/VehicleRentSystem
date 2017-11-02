using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRS.Model;
using VRS.Model.Repository;

namespace VRS.Logic.Controller
{
    public class RentController
    {
        private IRepository<Rent> repo = Repository<Rent>.GetInstance();

        public IQueryable<Rent> GetRentsForClient(Client client)
        {
            var result = new List<Rent>();
            if (client == null)
            {
                return result.AsQueryable();
            }

            return repo.SearchFor(r => r.ClientId == client.Id);
        }

        public Rent CreateRent(Rent rent, Client client)
        {
            var period = rent.FinishDate.Value.Subtract(rent.StartDate);
            var price = 100 * period.Days;
            rent.Price = price;
            rent.ClientId = client.Id;
            var result = repo.Insert(rent);
            if (result > 0)
            {
                return rent;
            }
            return null;
        }
    }
}
