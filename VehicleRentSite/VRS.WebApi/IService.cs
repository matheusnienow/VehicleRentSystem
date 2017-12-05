using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using VRS.Model;
using VRS.Repository.DTO;

namespace VRS.WebApi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        UserDTO VerifyUser(string username, string password);

        [OperationContract]
        IEnumerable<RentDTO> GetRents();

        [OperationContract]
        IEnumerable<UserDTO> GetUsers();

        [OperationContract]
        IEnumerable<ClientDTO> GetClients();

        [OperationContract]
        IEnumerable<VehicleDTO> GetVehicles();
    }
}
