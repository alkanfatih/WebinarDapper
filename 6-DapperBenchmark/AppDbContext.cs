using _6_DapperBenchmark.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_DapperBenchmark
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var context = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DapVsEn;Integrated Security=True;";
            optionsBuilder.UseSqlServer(context);
        }
    }
}
