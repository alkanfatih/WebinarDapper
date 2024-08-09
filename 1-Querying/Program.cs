using _1_Querying.Models;
using Dapper;
using System.Data.SqlClient;

namespace _1_Querying
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=Northwind;Integrated Security=True";

            #region ExecuteScalar
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();

            //    string sql = "SELECT COUNT(*) FROM Customers";
            //    //var customerCount = connection.ExecuteScalar(sql);
            //    int customerCount = connection.ExecuteScalar<int>(sql);

            //    Console.WriteLine($"Total number of customers: {customerCount}");
            //}
            #endregion

            #region QuerySingle
            //using (var connection = new SqlConnection(connectionString))
            //{
            //    var sql = "SELECT * FROM Products WHERE ProductID = @productId";
            //    var product = connection.QuerySingle(sql, new { productId = 1 });

            //    Console.WriteLine($"Product NAme: {product.ProductName}, Unit Price: {product.UnitPrice}");
            //}
            #endregion

            #region QuerySingle<T>
            //using (var connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();

            //    var sql = "SELECT CustomerID, CompanyName, ContactName, City FROM Customers WHERE CustomerID = @CustomerID";
            //    var customer = connection.QuerySingle<Customer>(sql, new { CustomerID = "ALFKI" });

            //    Console.WriteLine($"Customer ID {customer.CustomerID}");
            //    Console.WriteLine($"Company NAme {customer.CompanyName}");
            //    Console.WriteLine($"Contact NAme {customer.ContactName}");
            //    Console.WriteLine($"City {customer.City}");
            //}
            #endregion

            #region Query
            //using (var connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();

            //    var sql = "SELECT CustomerID, CompanyName, ContactName, City FROM Customers";
            //    var customers = connection.Query<Customer>(sql);

            //    foreach (var customer in customers)
            //    {
            //        Console.WriteLine($"Customer ID {customer.CustomerID}");
            //        Console.WriteLine($"Company NAme {customer.CompanyName}");
            //        Console.WriteLine($"Contact NAme {customer.ContactName}");
            //        Console.WriteLine($"City {customer.City}");
            //        Console.WriteLine(new string('-',20));
            //    }


            //}
            #endregion

            #region QueryMultiple

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"SELECT CustomerID, CompanyName, ContactName, City FROM Customers WHERE CustomerID = @CustomerID;
                               SELECT OrderID, CustomerID, OrderDate, ShipCity FROM Orders WHERE CustomerID = @CustomerID";

                using (var multi = connection.QueryMultiple(sql, new { CustomerID = "ALFKI" }))
                {
                    var customer = multi.ReadSingle<Customer>();
                    var orders = multi.Read<Order>();

                    Console.WriteLine($"Customer ID {customer.CustomerID}");
                    Console.WriteLine($"Company NAme {customer.CompanyName}");
                    Console.WriteLine($"Contact NAme {customer.ContactName}");
                    Console.WriteLine($"City {customer.City}");

                    foreach (var item in orders)
                    {
                        Console.WriteLine($"Order ID: {item.OrderID}");
                        Console.WriteLine($"Order Date: {item.OrderDate}");
                    }

                }
            }

            #endregion
        }
    }
}
