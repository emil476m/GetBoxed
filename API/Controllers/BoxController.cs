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
    [Route("/box/Search")]
    public IEnumerable<BoxFeed> SearchArticle([FromQuery][MinLength(3)]String searchTerm, [FromQuery][Range(0,int.MaxValue)]int amount)
    {
        return _service.SearchForBoxes(searchTerm, amount);
    }

    [HttpPost]
    [Route("/box")]
    public Box CreateBox([FromBody] Box box)
    {
        return _service.CreateBox(box.name,box.size,box.description,box.price,box.boxImgUrl);
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