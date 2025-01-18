using EcommerceApplication.Models;
using System.Data;
using System.Data.SqlClient;

namespace EcommerceApplication.Repository
{
    public interface ICustomerRepository
    {
        Task<bool> GetCustomerData(Customer customer);
    }
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<bool> GetCustomerData(Customer customer)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(""))
                {
                    using (SqlCommand cmd = new SqlCommand(""))
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CustomerId", SqlDbType.NVarChar).Value = customer.CustomerId;
                        cmd.Parameters.Add("@EmailId", SqlDbType.NVarChar).Value = customer.Email;

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            
        }
    }
}
