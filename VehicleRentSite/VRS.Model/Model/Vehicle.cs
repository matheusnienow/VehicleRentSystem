namespace VRS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using VRS.Repository.DTO;

    [Table("Vehicle")]
    public partial class Vehicle : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle()
        {
            Rent = new HashSet<Rent>();
        }

        [StringLength(100)]
        public string Description { get; set; }

        public int VehicleModelId { get; set; }

        [Required]
        [StringLength(7)]
        public string Plate { get; set; }

        public int? Mileage { get; set; }

        [Display(Name = "In Use")]
        public bool InUse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rent> Rent { get; set; }

        public virtual VehicleModel VehicleModel { get; set; }

        public VehicleDTO ToDto()
        {
            var dto = new VehicleDTO
            {
                Id = Id,
                Description = Description,
                InUse = InUse,
                Mileage = Mileage,
                Plate = Plate,
                VehicleModelId = VehicleModelId
            };
            return dto;
        }
    }
}
