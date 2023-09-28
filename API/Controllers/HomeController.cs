using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : Controller
{
    private readonly Service.Service _service;

    public HomeController(Service.Service service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("api/getAllBoxes")]
    public IEnumerator<string> getAllBoxes()
    {
        //TODO Make this Method and change return type
        return null;
    }

    [HttpGet]
    [Route("api/box/{boxId}")]
    public String getBoxByID([FromRoute] int boxID)
    {
        //TODO Make this Method and change return type
        return "Box" + boxID;
    }

  
}