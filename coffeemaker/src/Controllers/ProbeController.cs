using FP.ContainerTraining.CoffeeMaker.Business;
using Microsoft.AspNetCore.Mvc;
namespace FP.ContainerTraining.CoffeeMaker.Controllers;

[ApiController]
public class ProbeController : ControllerBase
{
    private readonly ILogger<CoffeeController> _logger;
    private readonly IOrderRepository _orderRepository;
    private static bool _isSick;

    public ProbeController(ILogger<CoffeeController> logger, IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    [HttpGet("~/probe/alive")]
    public IActionResult GetAlive()
    {
        if (_isSick)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Our barista is sick!");
        }
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpPut("~/probe/alive/{state}")]
    public IActionResult SetAlive([FromRoute] bool state)
    {
        _isSick = !state;
        return Accepted();
    }

    [HttpGet("~/probe/ready")]
    public IActionResult GetReady()
    {
        return _orderRepository.CanHandleOrder()
            ? StatusCode(StatusCodes.Status200OK)
            : StatusCode(StatusCodes.Status503ServiceUnavailable);
    }
}