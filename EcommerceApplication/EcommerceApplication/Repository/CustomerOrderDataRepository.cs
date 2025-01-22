using EcommerceApplication.Models;
using System.Data;
using System.Data.SqlClient;

namespace EcommerceApplication.Repository
{
    public interface ICustomerOrderDataRepository
    {
        Task<List<CustomerOrderProductMapping>> GetCustomerOrderMapping(Customer customer);

        
    }
    public class CustomerOrderDataRepository : ICustomerOrderDataRepository
    {
        public async Task<List<CustomerOrderProductMapping>> GetCustomerOrderMapping(Customer customer)
        {
            var customerOrderMappings = new List<CustomerOrderProductMapping>();

            try
            {
                using (SqlConnection conn = new SqlConnection(""))
                {
                    using (SqlCommand cmd = new SqlCommand("GetCustomerOrderDetails", conn)) 
                    {
                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                        await conn.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var orderMapping = new CustomerOrderProductMapping
                                {
                                    customerId = reader["CustomerId"] != DBNull.Value ? reader["CustomerId"].ToString() : string.Empty,
                                    firstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : string.Empty,
                                    lastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : string.Empty,
                                    orderId = reader["OrderId"] != DBNull.Value ? Convert.ToInt32(reader["OrderId"]) : 0,
                                    orderDate = reader["OrderDate"] != DBNull.Value ? Convert.ToDateTime(reader["OrderDate"]) : DateTime.MinValue,
                                    deliveryAddress = reader["DeliveryAddress"] != DBNull.Value ? reader["DeliveryAddress"].ToString() : string.Empty,
                                    orderItemId = reader["OrderItemId"] != DBNull.Value ? Convert.ToInt32(reader["OrderItemId"]) : 0,
                                    productId = reader["ProductId"] != DBNull.Value ? Convert.ToInt32(reader["ProductId"]) : 0,
                                    productName = reader["ProductName"] != DBNull.Value ? reader["ProductName"].ToString() : string.Empty,
                                    price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0.0m,
                                    quantity = reader["Quantity"] != DBNull.Value ? Convert.ToInt32(reader["Quantity"]) : 0,
                                    deliveryExpectedDate = reader["DeliveryExpectedDate"] != DBNull.Value ? Convert.ToDateTime(reader["DeliveryExpectedDate"]) : DateTime.MinValue,
                                    isGift = reader["IsGift"] != DBNull.Value ? Convert.ToBoolean(reader["IsGift"]) : false
                                };

                                customerOrderMappings.Add(orderMapping);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                
            }

            return customerOrderMappings;
        }

    }
}
