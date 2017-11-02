namespace VRS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rent")]
    public partial class Rent : BaseEntity
    {
        public int ClientId { get; set; }

        public int VehicleId { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Start")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "End")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FinishDate { get; set; }

        public double? Price { get; set; }

        public virtual Client Client { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
