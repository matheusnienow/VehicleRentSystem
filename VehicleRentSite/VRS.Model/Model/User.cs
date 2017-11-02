namespace VRS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Client = new HashSet<Client>();
        }

        public User(string login, byte[] salt, byte[] hash, int roleId)
        {
            Id = 0;
            Login = login;
            Salt = salt;
            Hash = hash;
            RoleId = roleId;
            Client = new HashSet<Client>();
        }

        [Required]
        [StringLength(20)]
        public string Login { get; set; }

        [Required]
        [MaxLength(256)]
        public byte[] Salt { get; set; }

        [Required]
        [MaxLength(32)]
        public byte[] Hash { get; set; }

        public int? RoleId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client> Client { get; set; }

        public virtual Role Role { get; set; }
    }
}
