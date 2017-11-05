namespace VRS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;
    using VRS.Repository.DTO;

    [Table("Role")]
    [DataContract]
    public partial class Role : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [DataMember]
        public string Name { get; set; }

        public RoleDTO ToDto()
        {
            return new RoleDTO(Id, Name);
        }
    }
}
