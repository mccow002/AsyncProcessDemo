using AsyncDemo.Services.Handlers.Orders.Commands;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;

namespace AsyncDemo.Api.Controllers;

[Route("order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IBus _bus;

    public OrderController(IBus bus)
    {
        _bus = bus;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateOrderRequest request)
    {
        try
        {
            await _bus.Send(request);
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}