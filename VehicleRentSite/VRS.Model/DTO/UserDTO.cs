using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VRS.Repository.DTO
{
    [DataContract]
    public class UserDTO
    {

        public UserDTO(int id, string login, byte[] salt, byte[] hash, int roleId, RoleDTO role)
        {
            Id = id;
            Login = login;
            Salt = salt;
            Hash = hash;
            RoleId = roleId;
            Role = role;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Login { get; set; }
        
        [DataMember]
        public byte[] Salt { get; set; }
        
        [DataMember]
        public byte[] Hash { get; set; }
        
        [DataMember]
        public int? RoleId { get; set; }

        [DataMember]
        public RoleDTO Role { get; set; }
    }
}
