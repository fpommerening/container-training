using FP.ContainerTraining.CoffeeMaker.Business;
using FP.ContainerTraining.CoffeeMaker.Models;
using Microsoft.AspNetCore.Mvc;

namespace FP.ContainerTraining.CoffeeMaker.Controllers;

[ApiController]
public class CoffeeController : Controller
{
    private readonly ILogger<CoffeeController> _logger;
    private readonly IOrderRepository _orderRepository;

    public CoffeeController(ILogger<CoffeeController> logger, IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    [HttpGet("~/order/")]
    public IActionResult Index()
    {
        var model = new OrdersViewModel
        {
            InstanceName = Environment.MachineName,
            Orders = _orderRepository.GetOrders().Select(x => new OrderViewModel
            {
                CreatedAt = x.CreatedAt,
                FinishedAt = x.FinishedAt,
                Product = x.Product
            }).ToArray()
        };
        return View(model);
    }

    [HttpPut("~/order/")]
        
    public IActionResult AddOrder([FromBody] OrderViewModel product)
    {
        if (!_orderRepository.CanHandleOrder())
        {
            _logger.LogWarning("Unable to order {product}", product);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
        _orderRepository.AddOrder(product.Product);
        _logger.LogInformation("Add order for {product}", product);
        return Accepted();
    }
}