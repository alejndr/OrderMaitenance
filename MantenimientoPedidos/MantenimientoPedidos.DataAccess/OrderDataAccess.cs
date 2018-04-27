using MantenimientoPedidos.DataAccess.Base;
using MantenimientoPedidos.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MantenimientoPedidos.DataAccess
{
    public class OrderDataAccess : DataAccessBase
    {
        /// <summary>
        /// Search for a date between dateFrom and dateTo.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<Order> Search(int customerId, DateTime dateFrom, DateTime dateTo)
        {
            // Init return value
            
            List<Order> orderList = new List<Order>();

            SqlConnection conn = new SqlConnection(base.ConnectionString);

            SqlDataReader reader = null;

            try
            {
                // Open connection 
                conn.Open();

                // Set query
                StringBuilder query = new StringBuilder();

                query.Append("SELECT     SalesOrderID as ID, OrderDate, SalesOrderNumber, ShipMethod.Name as ShipMethod, SalesOrderActive ");
                query.Append("FROM       Sales.SalesOrderHeader ");
                query.Append("inner join Purchasing.ShipMethod on SalesOrderHeader.ShipMethodID = ShipMethod.ShipMethodID ");
                query.Append("where      1 = 1 ");
                query.Append("AND        CustomerID = @customerId ");
                query.Append("AND        cast(sales.SalesOrderHeader.OrderDate as date) >= cast(@OrderDateFrom as date) ");
                query.Append("AND        cast(sales.SalesOrderHeader.OrderDate as date) <= cast(@OrderDateTo as date) ");

                
                // Init command
                SqlCommand cmd = new SqlCommand(query.ToString(), conn);

                SqlParameter paramId = new SqlParameter("@CustomerId", customerId);
                cmd.Parameters.Add(paramId);

                SqlParameter paramDateFrom = new SqlParameter("@OrderDateFrom", dateFrom);
                cmd.Parameters.Add(paramDateFrom);

                SqlParameter paramDateTo = new SqlParameter("@OrderDateTo", dateTo);
                cmd.Parameters.Add(paramDateTo);

                // Execute
                reader = cmd.ExecuteReader();

                // Read 
                while (reader.Read())
                {
                    Order actualOrder = new Order();

                    actualOrder.ID = Convert.ToInt32(reader["ID"]);

                    actualOrder.Date = Convert.ToDateTime(reader["OrderDate"]);

                    actualOrder.Number = reader["SalesOrderNumber"].ToString();

                    actualOrder.ShipMethod = reader["ShipMethod"].ToString();

                    actualOrder.Enable = (bool)reader["SalesOrderActive"];

                    orderList.Add(actualOrder);
                    
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




            return orderList;
        }

        /// <summary>
        /// Logical remove of a Order.
        /// </summary>
        /// <param name="orderToRemove"></param>
        public void RemoveOrder(Order orderToRemove)
        {

            SqlConnection conn = new SqlConnection(base.ConnectionString);

            try
            {
                // Open connection
                conn.Open();

                // Set query
                StringBuilder query = new StringBuilder();

                query.Append(" UPDATE	Sales.SalesOrderHeader ");
                query.Append(" SET		SalesOrderActive = 0 ");
                query.Append(" WHERE	SalesOrderID = @SalesOrderId ");
                query.Append(" AND		SalesOrderNumber = @SalesOrderNumber ");

                // Init command
                SqlCommand cmd = new SqlCommand(query.ToString(), conn);

                SqlParameter paramId = new SqlParameter("@SalesOrderId", orderToRemove.ID);
                cmd.Parameters.Add(paramId);

                SqlParameter paramOrderNumber = new SqlParameter("@SalesOrderNumber", orderToRemove.Number);
                cmd.Parameters.Add(paramOrderNumber);

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

        /// <summary>
        /// Get a list of the order details.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<OrderDetail> GetOrderDetail(int orderId)
        {
            // Init return value

            List<OrderDetail> orderDetailList = new List<OrderDetail>();

            SqlConnection conn = new SqlConnection(base.ConnectionString);

            SqlDataReader reader = null;

            try
            {

                // Open connection 
                conn.Open();

                // Set query
                StringBuilder query = new StringBuilder();

                
                query.Append("SELECT SalesOrderID, CarrierTrackingNumber, OrderQty, Product.Name as Name ");
                query.Append("FROM Sales.SalesOrderDetail ");
                query.Append("inner join Sales.SpecialOfferProduct on SalesOrderDetail.ProductID = SpecialOfferProduct.ProductID ");
                query.Append("inner join Production.Product on SpecialOfferProduct.ProductID = Product.ProductID ");
                query.Append("WHERE SalesOrderID = @OrderId ");


                // Init command
                SqlCommand cmd = new SqlCommand(query.ToString(), conn);

                SqlParameter paramId = new SqlParameter("@OrderId", orderId);
                cmd.Parameters.Add(paramId);

                
                // Execute
                reader = cmd.ExecuteReader();


                // Read 
                while (reader.Read())
                {
                    OrderDetail actualOrderDetail = new OrderDetail();

                    actualOrderDetail.ID = Convert.ToInt32(reader["SalesOrderID"]);
                    actualOrderDetail.TrackingNumber = reader["CarrierTrackingNumber"].ToString();
                    actualOrderDetail.OrderQty = Convert.ToInt32(reader["OrderQty"]);
                    actualOrderDetail.ProductName = reader["Name"].ToString();
                    
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
    }
}
