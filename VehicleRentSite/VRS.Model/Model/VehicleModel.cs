namespace VRS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;

    [Table("VehicleModel")]
    public partial class VehicleModel : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VehicleModel()
        {
            Vehicle = new HashSet<Vehicle>();
        }

        [Required]
        [StringLength(100)]
        [Display(Name = "Model")]
        public string Description { get; set; }

        public int? Year { get; set; }

        public int? BrandId { get; set; }

        public int? VehicleTypeId { get; set; }

        public int? GasTypeId { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual GasType GasType { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [XmlIgnore()]
        public virtual ICollection<Vehicle> Vehicle { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}
