namespace EcommerceApplication.Models
{
    public class Order
    {
        public int OrderId { get; set; } 
        public string CustomerId { get; set; } 
        public DateTime OrderDate { get; set; } 
        public DateTime DeliveryExpectedDate { get; set; } 
        public bool IsContainsGift { get; set; }

        public string DeliveryAddress { get; set; }
    }
}
