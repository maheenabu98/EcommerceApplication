namespace EcommerceApplication.Models
{
    public class CustomerOrderMapping
    {
        public Customer customer { get; set; }

        public OrderMapping orderProductMappings { get; set; }
    }
}
