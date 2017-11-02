namespace VRS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Rent = new HashSet<Rent>();
        }

        public Client(string name, string surname, string sex, string phone, string city, DateTime birthDate, int userId)
        {
            Name = name;
            Surname = surname;
            Sex = sex;
            Phone = phone;
            City = city;
            BirthDate = BirthDate;
            UserId = userId;

            Passport = "";
            CPF = 0;
            Address = "";
            Rent = new HashSet<Rent>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(10)]
        public string Passport { get; set; }

        public int? CPF { get; set; }

        [StringLength(1)]
        public string Sex { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        public int? UserId { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rent> Rent { get; set; }
    }
}
