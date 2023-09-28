using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;

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
    public IEnumerator<string> GetAllBoxes()
    {
        //TODO Make this Method and change return type
        return null;
    }

    [HttpGet]
    [Route("/box/{boxId}")]
    public String GetBoxById([FromRoute] int boxID)
    {
        //TODO Make this Method and change return type
        return "Box" + boxID;
    }

    [HttpGet]
    [Route("/box/price/{boxId}")]
    public float GetBoxPrice([FromRoute] int boxId)
    {
        //TODO implment this
        return 0;
    }

    [HttpPost]
    [Route("/box")]
    public string CreateBox([FromBody] string Box)
    {
        //Todo change type to Be when BE is made
        return Box;
    }

    [HttpPut]
    [Route("/box/{boxId}")]
    public string UpdateBox(string box)
    {
        //Todo change type to Be when BE is made
        return box;
    }

    [HttpDelete]
    [Route("/box/{boxId}")]
    public bool DeleteBox([FromRoute] int boxId)
    {
        //Todo implement
        return false;
    }
    
}