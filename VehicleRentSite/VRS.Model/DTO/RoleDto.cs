using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VRS.Repository.DTO
{
    [DataContract]
    public class RoleDTO
    {
        public RoleDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
