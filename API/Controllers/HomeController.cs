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
    public IEnumerator<string> getAllBoxes()
    {
        //TODO Make this Method
        return null;
    }

  
}