using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VRS.Repository.DTO
{
    [DataContract]
    public class VehicleDTO
    {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int VehicleModelId { get; set; }

        [DataMember]
        public string Plate { get; set; }

        [DataMember]
        public int? Mileage { get; set; }

        [DataMember]
        public bool InUse { get; set; }
        [DataMember]
        public int Id { get;  set; }
    }
}
