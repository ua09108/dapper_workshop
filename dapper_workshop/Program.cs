using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using System.Data;
using System.Data.SqlClient;


namespace dapper_workshop
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=.\SQLExpress;Initial Catalog=testdb;Trusted_Connection=True";
            SqlConnection objConnection = new SqlConnection(connectionString);

            //List all customers
            var customerList =  SqlMapper.Query(objConnection, "GetCustomers", commandType: CommandType.StoredProcedure).ToList();
            foreach (var customer in customerList)
            {
                Console.WriteLine(string.Format("{0}, {1}, {2}", customer.customer_id.ToString(), customer.name, customer.email));
            }

            //Insert Customer record
            DynamicParameters p = new DynamicParameters();
            p.Add("@name", "Andy");
            p.Add("@email", "info@bytutorial.com");
            SqlMapper.Query(objConnection, "InsertCustomer", p, commandType: CommandType.StoredProcedure);
            Console.WriteLine("Customer record has been inserted successfully");

            //Update customer record
            p = new DynamicParameters();
            p.Add("@customer_id", 1);
            p.Add("@name", "Andy");
            p.Add("@email", "info@bytutorial.com");
            SqlMapper.Query(objConnection, "UpdateCustomer", p, commandType: CommandType.StoredProcedure);
            Console.WriteLine("Customer record has been updated successfully");

            //Delete customer record
            p = new DynamicParameters();
            p.Add("@customer_id", 3);
            SqlMapper.Query(objConnection, "DeleteCustomer", p, commandType: CommandType.StoredProcedure);
            Console.WriteLine("Customer record has been deleted successfully");

            Console.ReadLine();
        }

        public class CustomerInfo
        {
            public int customer_id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
        }
    }
}
