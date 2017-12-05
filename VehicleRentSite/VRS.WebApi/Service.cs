using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using VRS.Logic.Controller;
using VRS.Model;
using VRS.Repository.DTO;

namespace VRS.WebApi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service : IService
    {
        public IEnumerable<RentDTO> GetRents()
        {
            var rentController = new RentController();
            return rentController.GetAllWithVehicleAndClient().Select(x => x.ToDto());
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var userController = new UserController();
            var users = userController.GetAll().ToList();
            var result = users.Select(u => u.ToDto());
            return result;
        }

        public UserDTO VerifyUser(string username, string password)
        {
            var loginController = new UserController();
            User user = loginController.VerifyUser(username, password);
            return user.ToDto();
        }
        public IEnumerable<ClientDTO> GetClients()
        {
            var clientController = new ClientController();
            return clientController.GetAll().Select(x => x.ToDto());
        }

        public IEnumerable<VehicleDTO> GetVehicles()
        {
            var vehicleController = new VehicleController();
            return vehicleController.GetAll().Select(x => x.ToDto());
        }

        public Result FinishRent(int id)
        {
            try { 
                var rentController = new RentController();
                bool success = rentController.FinishRent(id);
                if (success)
                {
                    return new Result(true, "Success");
                }
                return new Result(false, "An error ocurred");
            } 
            catch (Exception e)
            {
                return new Result(false, e.Message);
            }
        }
    }
}
