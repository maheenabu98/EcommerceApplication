using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcommerceApplication.Services;
using EcommerceApplication.Models;
using EcommerceApplication.DTO;

namespace EcommerceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;


        public CustomerOrderController(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        
        [HttpPost("getOrderDetails")]
        public async Task<IActionResult> GetCustomerOrderMapping(string customerId,string email)
        {
            Customer customer = new Customer();
            customer.CustomerId = customerId;
            customer.Email = email;
            CustomerOrderResponse customerOrderResponse = new CustomerOrderResponse();
            CustomerOrderMapping customerOrderMapping = new CustomerOrderMapping();
            customerOrderResponse = await _customerOrderService.GetCustomerOrderMapping(customer);

            customerOrderMapping = customerOrderResponse.customerOrderMapping;




            if (customerOrderResponse.status==false)
            {
               return BadRequest("Invalid User");
            }
            return Ok(customerOrderMapping);
        }
    }
}
