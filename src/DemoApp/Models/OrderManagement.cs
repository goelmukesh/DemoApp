using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Models
{
    public partial class OrderManagement:DbContext
    {
        public OrderManagement(DbContextOptions<OrderManagement> options):base(options)
        { }

        public virtual DbSet<Customer> TblCustomer { get; set; }
        public virtual DbSet<Order> TblOrder { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity => entity.ToTable("TblCustomer"));
            modelBuilder.Entity<Order>(entity => entity.ToTable("TblOrder"));
        }
    }
}
