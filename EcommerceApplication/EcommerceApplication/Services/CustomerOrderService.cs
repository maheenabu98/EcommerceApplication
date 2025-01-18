
using EcommerceApplication.Models;
using EcommerceApplication.Repository;
using EcommerceApplication.DTO;
namespace EcommerceApplication.Services
{

    public interface ICustomerOrderService
    {
        Task<CustomerOrderResponse> GetCustomerOrderMapping(Customer customer);
    }
    public class CustomerOrderService : ICustomerOrderService
    {
        public readonly ICustomerRepository _customerRepository;
        public readonly ICustomerOrderDataRepository _customerOrderRepository;

        public CustomerOrderService(ICustomerRepository customerRepository, ICustomerOrderDataRepository customerOrderRepository)
        {
            _customerRepository = customerRepository;
            _customerOrderRepository = customerOrderRepository;
        }

        public async Task<CustomerOrderResponse> GetCustomerOrderMapping(Customer customer)
        {
            bool IsValidCustomer;
            CustomerOrderResponse customerOrder = new CustomerOrderResponse();
            IsValidCustomer = await _customerRepository.GetCustomerData(customer);
           

           
            if (IsValidCustomer)
            {
                List<CustomerOrderProductMapping> customerOrderMapping = new List<CustomerOrderProductMapping>();
                customerOrderMapping = await _customerOrderRepository.GetCustomerOrderMapping();
                customerOrder=GetCustomerOrderResponse(customerOrderMapping);

            }
            else
            {
                customerOrder.status = false;
            }


            return customerOrder;
        }

        public CustomerOrderResponse GetCustomerOrderResponse(List<CustomerOrderProductMapping> customerOrderProductMappings)
        {
            CustomerOrderResponse customerOrder = new CustomerOrderResponse();
            bool blnGift;
            CustomerOrderMapping customerOrderMapping = new CustomerOrderMapping
            {
                customer = new Customer(),
                orderProductMappings = new OrderMapping()
                
            };

            customerOrderMapping.customer.FirstName= customerOrderProductMappings.Select(x=>x.firstName).Distinct().First();
            customerOrderMapping.customer.LastName = customerOrderProductMappings.Select(x => x.lastName).Distinct().First();

            customerOrderMapping.orderProductMappings.order.OrderId= customerOrderProductMappings.Select(x => x.orderId).Distinct().First();
            customerOrderMapping.orderProductMappings.order.OrderDate= customerOrderProductMappings.Select(x => x.orderDate).Distinct().First();
            customerOrderMapping.orderProductMappings.order.DeliveryAddress= customerOrderProductMappings.Select(x => x.deliveryAddress).Distinct().First();
            customerOrderMapping.orderProductMappings.order.DeliveryExpectedDate= customerOrderProductMappings.Select(x => x.deliveryExpectedDate).Distinct().First();
            blnGift = customerOrderProductMappings.Select(x => x.isGift).Distinct().First();
            List<int> orderItemDetailIds = customerOrderProductMappings.Select(x=>x.orderItemId).Distinct().ToList();

            foreach(int orderItemDetailId in orderItemDetailIds)
            {
                string productName = "";
                if (blnGift == true)
                {
                     productName = "Gift";
                }
                else
                {
                     productName = customerOrderProductMappings.Where(x => x.orderItemId == orderItemDetailId)
                   .Select(x => x.productName).FirstOrDefault();
                }
               
                int quantity= customerOrderProductMappings.Where(x => x.orderItemId == orderItemDetailId)
                    .Select(x => x.quantity).FirstOrDefault();
                decimal price= customerOrderProductMappings.Where(x => x.orderItemId == orderItemDetailId)
                    .Select(x => x.price).FirstOrDefault();
                customerOrderMapping.orderProductMappings.items.Add( new OrderProductMapping
                { ProductName =productName,
                   Quantity= quantity,
                   Price= price});
            }
            customerOrder.status = true;
            customerOrder.customerOrderMapping = customerOrderMapping;

            return customerOrder;
        }
    }
}
