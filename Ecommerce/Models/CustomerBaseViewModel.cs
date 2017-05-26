namespace Ecommerce.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CustomerBaseViewModel : DbContext
    {
        public CustomerBaseViewModel()
            : base("name=CustomerBaseViewModel")
        {
        }

        public virtual DbSet<address> address { get; set; }
        public virtual DbSet<orderProduct> orderProduct { get; set; }
        public virtual DbSet<orders> orders { get; set; }
        public virtual DbSet<product> product { get; set; }
        public virtual DbSet<shoppingCart> shoppingCart { get; set; }
        public virtual DbSet<shoppingCartProduct> shoppingCartProduct { get; set; }
        public virtual DbSet<state> state { get; set; }
        public virtual DbSet<status> status { get; set; }
        public virtual DbSet<users> users { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<address>()
                .Property(e => e.Address1)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.Address2)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.Address3)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.ZipCode)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<orderProduct>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<orderProduct>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<orderProduct>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<orders>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<orders>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<orders>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.ProductDescription)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<product>()
                .Property(e => e.ImageFile)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<shoppingCart>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<shoppingCart>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<shoppingCartProduct>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<shoppingCartProduct>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<state>()
                .Property(e => e.StateAbbrev)
                .IsUnicode(false);

            modelBuilder.Entity<state>()
                .Property(e => e.StateName)
                .IsUnicode(false);

            modelBuilder.Entity<status>()
                .Property(e => e.StatusDescription)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);
        }
    }
}
