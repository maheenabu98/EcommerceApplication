namespace EcommerceApplication.Models
{
    public class CustomerOrderProductMapping
    {
        public string customerId { get; set; }
        public string firstName { get; set; }

        public string lastName { get; set; }

        public int orderId { get; set; }

        public DateTime orderDate { get; set; }

        public string deliveryAddress { get; set; }

        public int orderItemId { get; set; }
        public int productId { get; set; }

        public string productName { get; set; }

        public decimal price { get; set; }

        public int quantity { get; set; }

        public DateTime deliveryExpectedDate    { get; set; }

        public bool isGift { get; set; }
    }
}
