using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;



namespace API.Controllers;

public class BoxController : Controller
{
    //private readonly ILogger<BoxController> _logger;
    
    private readonly Service.Service _service;

    public BoxController(Service.Service service)
    {
        _service = service;
    }

    
}