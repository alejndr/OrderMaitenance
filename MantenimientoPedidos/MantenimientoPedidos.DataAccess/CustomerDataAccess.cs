using MantenimientoPedidos.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MantenimientoPedidos.Entities;

namespace MantenimientoPedidos.DataAccess
{
    public class CustomerDataAccess : DataAccessBase
    {

        /// <summary>
        /// Validate if exist the introduced id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool ValidateExistCustomer(int customerId)
        {
            // Init return value
            bool exist = false;

            SqlConnection conn = new SqlConnection(base.ConnectionString);

            try
            {
                // Open connection
                conn.Open();

                // Set query
                StringBuilder query = new StringBuilder();

                query.Append("SELECT COUNT(CustomerID) FROM Sales.Customer ");
                query.Append(" WHERE CustomerID = @customerId");

                // Init command
                SqlCommand cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandText = query.ToString()
                };

                SqlParameter paramId = new SqlParameter("@customerId", customerId);
                cmd.Parameters.Add(paramId);

                // Execute
                int count = (int)cmd.ExecuteScalar();

                exist = (count > 0);

            }
            catch
            {
                throw;

            }
            finally
            {
                // Close connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return exist;
        }


        /// <summary>
        /// Reads the name, store and territory of the customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer GetCustomer(int customerId)
        {
            Customer customerData = new Customer();

            SqlConnection conn = new SqlConnection(base.ConnectionString);

            SqlDataReader reader = null;

            try
            {
                // Open connection
                conn.Open();

                //Set query
                StringBuilder query = new StringBuilder();

                query.Append("SELECT    Person.Person.FirstName as FirstName, Sales.Store.Name as StoreName, Sales.SalesTerritory.Name as TerritoryName "); //CustomerId, FirstName, StoreName, TerritoryName
                query.Append("FROM      Sales.Customer ");
                query.Append("left join Person.Person on Person.BusinessEntityID = Customer.CustomerID ");
                query.Append("left join Sales.Store on StoreID = Store.BusinessEntityID ");
                query.Append("left join sales.SalesTerritory on Customer.TerritoryID = SalesTerritory.TerritoryID ");
                query.Append("WHERE     CustomerID = @customerId");

                // Init command
                SqlCommand cmd = new SqlCommand(query.ToString(), conn);

                 SqlParameter paramId = new SqlParameter("@CustomerId", customerId);
                 cmd.Parameters.Add(paramId);

                // Execute
                reader = cmd.ExecuteReader();

                // Read
                if (reader.Read())
                {
                    customerData.Name = reader["FirstName"].ToString();

                    customerData.Store = reader["StoreName"].ToString();

                    customerData.Territory = reader["TerritoryName"].ToString();
                }

            }
            catch
            {

                throw;
            }
            finally
            {
                // Close datareader
                if (reader != null)
                {
                    reader.Close();
                }

                // Close connection
                if (conn != null)
                {
                    conn.Close();
                }
            }


            return customerData;
        }

    }
}
