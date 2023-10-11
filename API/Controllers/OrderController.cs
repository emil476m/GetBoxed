using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Infarstructure;
using Service;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : Controller
{
    private readonly OrderService _service;

    public OrderController(OrderService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("/Order")]
    public IEnumerable<OrderFeed> GetAllOrders()
    {
        return _service.getAllOrders();
    }
    
    [HttpGet]
    [Route("/Order/Customer/{customerId}")]
    public IEnumerable<Order> GetOrderByCostomerId([FromRoute] int customerId)
    {
        return _service.getAllOrdersByCustomerId(customerId);
    }
    
    [HttpGet]
    [Route("/Order/{orderId}")]
    public Order GetOrderById([FromRoute] int orderId)
    {
        return _service.getOrderById(orderId);
    }
    
    [HttpPost]
    [Route("/order")]
    public Order CreateOrder([FromBodyAttribute] Order order)
    {
        return _service.createOrder(order.customerId,order.totalPrice, order.orderDate,order.BoxOrder);
    }
    
    [HttpPost]
    [Route("/customer")]
    public Customer CreateCustomer([FromBody] Customer customer)
    {
        return _service.CreateCustomer(customer.name, customer.mail, customer.tlf, customer.address);
    }
    
}