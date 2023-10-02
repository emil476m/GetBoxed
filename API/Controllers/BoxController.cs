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
    public Box GetBoxById([FromRoute] int boxID)
    {
        //TODO Make this Method and change return type
        return null;
    }

    [HttpGet]
    [Route("/box/price/{boxId}")]
    public float GetBoxPrice([FromRoute] int boxId)
    {
        //TODO implment this
        return -1;
    }

    [HttpPost]
    [Route("/box")]
    public Box CreateBox([FromBody] Box box)
    {
        //Todo change type to Be when BE is made
        return _service.CreateBox(box.name,box.size,box.description,box.price,box.boxImgUrl);
    }

    [HttpPut]
    [Route("/box/{boxId}")]
    public Box UpdateBox(Box box, [FromRoute] int boxId)
    {
        //Todo change type to Be when BE is made
        return box;
    }

    [HttpDelete]
    [Route("/box/{boxId}")]
    public bool DeleteBox([FromRoute] int boxId)
    {
        return _service.DeleteBoxById(boxId);
    }
    
}