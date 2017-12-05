using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using VRS.Model;

namespace VRS.Repository.DTO
{
    [DataContract]
    public partial class RentDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public int VehicleId { get; set; }
        
        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime? FinishDate { get; set; }

        [DataMember]
        public double? Price { get; set; }

        [DataMember]
        public bool Finished { get; set; }

        [DataMember]
        public ClientDTO Client { get; set; }

        [DataMember]
        public VehicleDTO Vehicle { get; set; }
    }
}
