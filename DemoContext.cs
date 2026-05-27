using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace demo27._05
{
    public partial class DemoContext : DbContext
    {
        public DemoContext()
            : base("name=demoContext")
        {
        }

        public virtual DbSet<Category> categories { get; set; }
        public virtual DbSet<Fabric> fabrics { get; set; }
        public virtual DbSet<Label> labels { get; set; }
        public virtual DbSet<OrderDetails> order_details { get; set; }
        public virtual DbSet<Order> orders { get; set; }
        public virtual DbSet<PickupPoints> pickup_points { get; set; }
        public virtual DbSet<Product> products { get; set; }
        public virtual DbSet<Role> roles { get; set; }
        public virtual DbSet<Status> statuses { get; set; }
        public virtual DbSet<Supplier> suppliers { get; set; }
        public virtual DbSet<Sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.products)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasRequired(e => e.Category)
                .WithMany(e => e.products)
                .HasForeignKey(e => e.category_id);

            modelBuilder.Entity<Product>()
       .HasRequired(e => e.Supplier)
       .WithMany(e => e.products)
       .HasForeignKey(e => e.supplier_id);

            modelBuilder.Entity<Product>()
                .HasRequired(e => e.Fabric)
                .WithMany(e => e.products)
                .HasForeignKey(e => e.fabric_id);

            modelBuilder.Entity<Product>()
                .HasRequired(e => e.Label)
                .WithMany(e => e.products)
                .HasForeignKey(e => e.label_id);

            modelBuilder.Entity<Fabric>()
                .HasMany(e => e.products)
                .WithRequired(e => e.Fabric)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Label>()
                .HasMany(e => e.products)
                .WithRequired(e => e.Label)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.order_details)
                .WithRequired(e => e.order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PickupPoints>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.pickup_points)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.article)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.price)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Order_details)
                .WithRequired(e => e.product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.users)
                .WithRequired(e => e.role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.products)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);
        }
    }
}
