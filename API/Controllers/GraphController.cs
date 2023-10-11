using Infarstructure;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GraphController : Controller
{
    private readonly GraphSrevice _service;

    public GraphController(GraphSrevice service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("/GraphData")]
    public IEnumerable<graphDataBox> getgraphData()
    {
        return _service.getGraphDataForAllBoxes();
    }
}