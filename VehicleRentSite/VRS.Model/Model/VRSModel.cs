namespace VRS.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class VRSModel : DbContext
    {
        public VRSModel()
            : base("name=VRSModel")
        {
        }

        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<GasType> GasType { get; set; }
        public virtual DbSet<Rent> Rent { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<VehicleModel> VehicleModel { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.Surname)
                .IsFixedLength();

            modelBuilder.Entity<Client>()
                .Property(e => e.Passport)
                .IsFixedLength();

            modelBuilder.Entity<Client>()
                .Property(e => e.Sex)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Rent)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Salt)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Hash)
                .IsFixedLength();

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.Rent)
                .WithRequired(e => e.Vehicle)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VehicleModel>()
                .HasMany(e => e.Vehicle)
                .WithRequired(e => e.VehicleModel)
                .WillCascadeOnDelete(false);
        }
    }
}
