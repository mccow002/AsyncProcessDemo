using AsyncDemo.Services.Handlers.Orders.Commands.CreateOrder;
using AsyncDemo.Services.Handlers.Orders.Queries.GetOrders;
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
    private readonly IMediator _mediator;

    public OrderController(IBus bus, IMediator mediator)
    {
        _bus = bus;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _mediator.Send(new GetOrdersRequest()));
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

    [HttpPut]
    public async Task<IActionResult> Put()
    {
        return Ok();
    }

    [HttpDelete("{orderId}")]
    public async Task<IActionResult> Delete(int orderId)
    {
        return Ok();
    }
}