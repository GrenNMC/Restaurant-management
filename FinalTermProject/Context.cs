using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FinalTermProject
{
    public partial class Context : DbContext
    {
        public Context() : base("name = QLNH7Entities")
        {
            Database.SetInitializer<Context>(new Initializer<Context>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntityClass.OrderDetail>()
                .HasKey(c => new { c.OrderID, c.ProductID });
            modelBuilder.Entity<EntityClass.BookingDetail>()
                .HasKey(c => new { c.CustomerID, c.TableID });
            modelBuilder.Entity<EntityClass.Customer>()
                .HasIndex(c => c.Phone)
                .IsUnique(true);
            modelBuilder.Entity<EntityClass.Employee>()
                .HasIndex(e => e.HomePhone)
                .IsUnique(true);
        }
        public virtual DbSet<EntityClass.C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<EntityClass.BookingDetail> BookingDetails { get; set; }
        public virtual DbSet<EntityClass.Category> Categories { get; set; }
        public virtual DbSet<EntityClass.Customer> Customers { get; set; }
        public virtual DbSet<EntityClass.Employee> Employees { get; set; }
        public virtual DbSet<EntityClass.OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<EntityClass.Order> Orders { get; set; }
        public virtual DbSet<EntityClass.Product> Products { get; set; }
        public virtual DbSet<EntityClass.Table> Tables { get; set; }
        public virtual DbSet<EntityClass.User> Users { get; set; }
    }
}
