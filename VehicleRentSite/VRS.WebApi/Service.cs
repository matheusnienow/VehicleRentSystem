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
        public List<RentDTO> GetRents()
        {
            var rentController = new RentController();
            return rentController.GetAllWithVehicleAndClient().ToList().Select(x => x.ToDto()).ToList();
        }

        public List<UserDTO> GetUsers()
        {
            var userController = new UserController();
            return userController.GetAll().Select(u => u.ToDto()).ToList();
        }

        public UserDTO VerifyUser(string username, string password)
        {
            var loginController = new UserController();
            User user = loginController.VerifyUser(username, password);
            return user.ToDto();
        }
    }
}
