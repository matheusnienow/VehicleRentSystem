using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VRS.Repository.DTO
{
    [DataContract]
    public class ClientDTO
    {
        [DataMember]
        public int Id{ get; set; }

        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public string Passport { get; set; }

        [DataMember]
        public int? CPF { get; set; }

        [DataMember]
        public string Sex { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public DateTime? BirthDate { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public int? UserId { get; set; }
    }
}
