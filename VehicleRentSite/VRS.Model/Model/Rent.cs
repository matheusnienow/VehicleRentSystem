namespace VRS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;
    using VRS.Repository.DTO;

    [Table("Rent")]
    [DataContract]
    public partial class Rent : BaseEntity
    {
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int VehicleId { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Start")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataMember]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "End")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataMember]
        public DateTime? FinishDate { get; set; }

        [DataMember]
        public double? Price { get; set; }

        [DataMember]
        public bool Finished { get; set; }

        [DataMember]
        public virtual Client Client { get; set; }

        [DataMember]
        public virtual Vehicle Vehicle { get; set; }

        public RentDTO ToDto()
        {
            var dto = new RentDTO
            {
                Id = Id,
                Price = Price,
                StartDate = StartDate,
                FinishDate = FinishDate,
                ClientId = ClientId,
                VehicleId = VehicleId,
                Finished = Finished,
                Client = Client.ToDto(),
                Vehicle = Vehicle.ToDto()
            };
            return dto;
        }
    }
}
