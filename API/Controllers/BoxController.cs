using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Infarstructure;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoxController : Controller
{
    private readonly Service.Service _service;

    public BoxController(Service.Service service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("/box")]
    public IEnumerable<BoxFeed> GetAllBoxes()
    {
        return _service.getBoxFeed();
    }

    [HttpGet]
    [Route("/box/{boxId}")]
    public Box GetBoxById([FromRoute] int boxId)
    {
        return _service.GetBoxById(boxId);
    }

    [HttpGet]
    [Route("/box/price/{boxId}")]
    public float GetBoxPrice([FromRoute] int boxId)
    {
        return _service.GetPriceOfBox(boxId);
    }
    
    [HttpGet]
    [Route("/box/Seartch")]
    public IEnumerable<BoxFeed> SearchArticle([FromQuery][MinLength(3)]String searchTerm, [FromQuery][Range(0,int.MaxValue)]int amount)
    {
        return _service.SearchForBoxes(searchTerm, amount);
    }
    
    [HttpGet]
    [Route("/Order")]
    public IEnumerable<OrderFeed> GetAllOrders()
    {
        //TODO Get all orders
        //return _service.getAllOrders();
        throw new NotImplementedException();
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
    [Route("/box")]
    public Box CreateBox([FromBody] Box box)
    {
        return _service.CreateBox(box.name,box.size,box.description,box.price,box.boxImgUrl);
    }

    [HttpPost]
    [Route("/order")]
    public Order CreateOrder([FromBodyAttribute] Order order)
    {
        return _service.createOrder(order.customerId,order.totalPrice, order.BoxOrder);
    }

    [HttpPut]
    [Route("/box/{boxId}")]
    public Box UpdateBox(Box box, [FromRoute] int boxId)
    {
        return _service.UpdateBox(boxId,box);
    }

    [HttpDelete]
    [Route("/box/{boxId}")]
    public bool DeleteBox([FromRoute] int boxId)
    {
        return _service.DeleteBoxById(boxId);
    }
    
}