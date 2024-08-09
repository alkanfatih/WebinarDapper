using _6_DapperBenchmark.Models;
using BenchmarkDotNet.Attributes;
using Bogus;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_DapperBenchmark
{
    public class OrmPerformanceTest
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DapVsEn;Integrated Security=True;";

        private List<Product> GenerateFakeProducts(int count)
        {
            var faker = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()));

            return faker.Generate(count);
        }

        [GlobalSetup]
        public void Setup()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("DELETE FROM Products");
            }
        }

        [Benchmark]
        public async Task EfCoreInsert()
        {
            var products = GenerateFakeProducts(10);

            using (var context = new AppDbContext())
            {
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }

        [Benchmark]
        public async Task DapperInsert()
        {
            var products = GenerateFakeProducts(10);

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                foreach (var product in products)
                {
                    await connection.ExecuteAsync("INSERT INTO Products (Name, Price) VALUES (@Name, @Price)", product);
                }
            }
        }

        [Benchmark]
        public async Task EfCoreRead()
        {
            using (var context = new AppDbContext())
            {
                var products = await context.Products.ToListAsync();
            }
        }

        [Benchmark]
        public async Task DapperRead()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var products = await connection.QueryAsync<Product>("SELECT * FROM Products");
            }
        }
    }
}
