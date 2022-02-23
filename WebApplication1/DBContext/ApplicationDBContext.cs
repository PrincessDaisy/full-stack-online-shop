using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.DBContext
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Order> Order { get; set; }

        public DbSet<Cart> Cart { get; set; }

        public DbSet<OrderProduct> OrderProduct { get; set; }

        public DbSet<OrderHistory> OrderHistory { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

    }
}
