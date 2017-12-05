namespace VRS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;
    using VRS.Repository.DTO;

    [Table("User")]
    [DataContract]
    public partial class User : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
        }

        public User(string login, byte[] salt, byte[] hash, int roleId)
        {
            Id = 0;
            Login = login;
            Salt = salt;
            Hash = hash;
            RoleId = roleId;
        }

        [Required]
        [StringLength(20)]
        [DataMember]
        public string Login { get; set; }

        public UserDTO ToDto()
        {
            return new UserDTO(Id, Login, Salt, Hash, RoleId.Value/*, Role.ToDto()*/);
        }

        [Required]
        [MaxLength(256)]
        [DataMember]
        public byte[] Salt { get; set; }

        [Required]
        [MaxLength(32)]
        [DataMember]
        public byte[] Hash { get; set; }

        [DataMember]
        public int? RoleId { get; set; }

        [DataMember]
        public virtual Role Role { get; set; }

    }
}
