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
        private IRepository<Rent> repo = Repository<Rent>.NewInstance();

        public IQueryable<Rent> GetRentsForClient(Client client)
        {
            var result = new List<Rent>();
            if (client == null)
            {
                return result.AsQueryable();
            }

            return repo.GetItems(r => r.ClientId == client.Id, r => r.Vehicle);
        }

        public List<Rent> GetAll()
        {
            return repo.GetAll().ToList();
        }

        public List<Rent> GetAllWithVehicleAndClient()
        {
            return repo.GetAll(r => r.Vehicle, r => r.Client).ToList();
        }

        public Rent CreateRent(Rent rent, Client client)
        {
            var period = rent.FinishDate.Value.Subtract(rent.StartDate);
            var price = 100 * period.Days;
            rent.Price = price;
            rent.ClientId = client.Id;
            rent.Finished = false;
            var result = repo.Insert(rent);
            if (result > 0)
            {
                var vehicleRepo = Repository<Vehicle>.NewInstance();
                var vehicle = vehicleRepo.GetById(rent.VehicleId);
                vehicle.InUse = true;
                var updateResult = vehicleRepo.Update(vehicle);                
                return rent;
            }
            return null;
        }

        public Rent CreateRent(Rent rent)
        {
            var vehicleRepo = Repository<Vehicle>.NewInstance();
            var vehicle = vehicleRepo.GetById(rent.VehicleId);
            if (vehicle.InUse)
            {
                return null;
            }

            var result = repo.Insert(rent);
            if (result > 0)
            {
                vehicle.InUse = true;
                var updateResult = vehicleRepo.Update(vehicle);
                return rent;
            }
            return null;
        }

        public bool FinishRent(int id)
        {
            var rent = repo.GetById(id);
            rent.Finished = true;
            rent.Vehicle.InUse = false;
            var result = repo.Update(rent);

            return result > 0;
        }
    }
}
