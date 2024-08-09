using _2_Non_Query.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace _2_Non_Query
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=Northwind;Integrated Security=True";

            #region Insert
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    string sql = "INSERT INTO Customers (CustomerID, CompanyName, ContactName, City) VALUES (@CustomerID, @CompanyName, @ContactName, @City)";

            //    var affectedRows = connection.Execute(sql, new { CustomerID = "NEWID", CompanyName = "New Company", ContactName = "New Contact", City = "New City" });

            //    Console.WriteLine($"Inserted {affectedRows} rows");
            //}
            #endregion

            #region Update
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    string sql = "UPDATE Customers SET CompanyName = @CompanyName WHERE CustomerID = @CustomerID";

            //    var affectedRows = connection.Execute(sql, new {CompanyName = "Updated Company", CustomerID = "NEWID" });

            //    Console.WriteLine($"Updated {affectedRows} rows");
            //}
            #endregion

            #region Delete
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    string sql = "DELETE FROM Customers WHERE CustomerID = @CustomerID";

            //    var affectedRows = connection.Execute(sql, new {CustomerID = "NEWID" });

            //    Console.WriteLine($"Deleted {affectedRows} rows");
            //}
            #endregion

            #region StoredProcedure
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string storedProcedure = "GetCustomersByCity";
                var parameters = new { City = "London" };

                IEnumerable<Customer> customers = connection.Query<Customer>(storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                foreach (Customer customer in customers) 
                {
                    Console.WriteLine($"Customer ID: {customer.CustomerID}");
                    Console.WriteLine($"Company Name: {customer.CompanyName}");
                    Console.WriteLine($"Contact Name: {customer.ContactName}");
                    Console.WriteLine(new string('-', 20));
                }
            }
            #endregion
        }
    }
}
