namespace EcommerceApplication.Models
{
    public class OrderMapping
    {
        public Order order { get; set; }

        public List<OrderProductMapping> items { get; set; }
    }
}
