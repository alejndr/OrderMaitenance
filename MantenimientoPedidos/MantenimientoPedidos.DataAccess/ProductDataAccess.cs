using MantenimientoPedidos.DataAccess.Base;
using MantenimientoPedidos.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoPedidos.DataAccess
{
    public class ProductDataAccess : DataAccessBase
    {

        /// <summary>
        /// Get a list of the order details.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<Product> GetProduct(int orderId)
        {
            // Init return value

            List<Product> orderDetailList = new List<Product>();

            SqlConnection conn = new SqlConnection(base.ConnectionString);

            SqlDataReader reader = null;

            try
            {

                // Open connection 
                conn.Open();

                // Set query
                StringBuilder query = new StringBuilder();


                query.Append("SELECT        SalesOrderID, CarrierTrackingNumber, OrderQty, Product.Name as Name, SalesOrderDetail.SalesOrderDetailID as SalesOrderDetailID ");
                query.Append("FROM          Sales.SalesOrderDetail ");
                query.Append("inner join    Sales.SpecialOfferProduct on SalesOrderDetail.ProductID = SpecialOfferProduct.ProductID ");
                query.Append("inner join    Production.Product on SpecialOfferProduct.ProductID = Product.ProductID ");
                query.Append("WHERE         SalesOrderID = @OrderId ");
                query.Append("group by SalesOrderID, CarrierTrackingNumber, OrderQty, Name, SalesOrderDetailID ");


                // Init command
                SqlCommand cmd = new SqlCommand(query.ToString(), conn);

                SqlParameter paramId = new SqlParameter("@OrderId", orderId);
                cmd.Parameters.Add(paramId);


                // Execute
                reader = cmd.ExecuteReader();


                // Read 
                while (reader.Read())
                {
                    Product actualOrderDetail = new Product();

                    actualOrderDetail.ID = Convert.ToInt32(reader["SalesOrderID"]);
                    actualOrderDetail.TrackingNumber = reader["CarrierTrackingNumber"].ToString();
                    actualOrderDetail.OrderQty = Convert.ToInt32(reader["OrderQty"]);
                    actualOrderDetail.ProductName = reader["Name"].ToString();
                    //actualOrderDetail.SpecialOfferID = Convert.ToInt32(reader["SpecialOfferID"]);
                    actualOrderDetail.SalesOrderDetailID = Convert.ToInt32(reader["SalesOrderDetailID"]);


                    orderDetailList.Add(actualOrderDetail);

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


            return orderDetailList;
        }

        /// <summary>
        /// Remove a product fisically.
        /// </summary>
        /// <param name="product"></param>
        public void RemoveProduct(Product product)
        {
            SqlConnection conn = new SqlConnection(base.ConnectionString);

            try
            {
                // Open connection
                conn.Open();

                // Set query
                StringBuilder query = new StringBuilder();

                query.Append(" DELETE       sal ");
                query.Append(" FROM         Sales.SalesOrderDetail as sal ");
                query.Append(" WHERE        SalesOrderID = @salesOrderId ");
                query.Append(" AND          SalesOrderDetailID = @salesOrderDetailId ");

                // Init command
                SqlCommand cmd = new SqlCommand(query.ToString(), conn);

                SqlParameter paramSalesOrderId = new SqlParameter("@salesOrderId", product.ID);
                cmd.Parameters.Add(paramSalesOrderId);
                
                SqlParameter paramSalesOrderDetailId = new SqlParameter("@salesOrderDetailId", product.SalesOrderDetailID);
                cmd.Parameters.Add(paramSalesOrderDetailId);

                // Execute
                cmd.ExecuteNonQuery();

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
        }

        public void ModifyOrderQty(Product product, int orderQty)
        {
            SqlConnection conn = new SqlConnection(base.ConnectionString);

            try
            {
                // Open connection
                conn.Open();

                // Set query
                StringBuilder query = new StringBuilder();

                query.Append(" UPDATE	Sales.SalesOrderDetail ");
                query.Append(" SET		OrderQty = @OrderQty, ");
                query.Append(" 		    ModifiedDate = getdate() ");
                query.Append(" WHERE	SalesOrderID = @SalesOrderId ");
                query.Append(" AND		SalesOrderDetailID = @SalesOrderDetailID ");

                // Init command
                SqlCommand cmd = new SqlCommand(query.ToString(), conn);

                SqlParameter paramOrderId = new SqlParameter("@SalesOrderId", product.ID);
                cmd.Parameters.Add(paramOrderId);

                SqlParameter paramOrderDetailId = new SqlParameter("@SalesOrderDetailID", product.SalesOrderDetailID);
                cmd.Parameters.Add(paramOrderDetailId);

                SqlParameter paramOrderQty = new SqlParameter("@OrderQty", orderQty);
                cmd.Parameters.Add(paramOrderQty);

                // Execute
                cmd.ExecuteNonQuery();
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
        }

    }
}
