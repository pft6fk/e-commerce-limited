using e_commerce.Api.Contracts;
using e_commerce.Application.Orders.Commands.AddOrderItem;
using e_commerce.Application.Orders.Commands.CancelOrder;
using e_commerce.Application.Orders.Commands.CompleteOrder;
using e_commerce.Application.Orders.Commands.CreateOrder;
using e_commerce.Application.Orders.Commands.PayOrder;
using e_commerce.Application.Orders.Commands.RemoveOrderItem;
using e_commerce.Application.Orders.Commands.ShipOrder;
using e_commerce.Application.Orders.Queries.GetOrderById;
using e_commerce.Application.Orders.Queries.GetOrdersByCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ISender _sender;

    public OrdersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request, CancellationToken ct)
    {
        var orderId = await _sender.Send(new CreateOrderCommand(request.CustomerId), ct);
        return CreatedAtAction(nameof(GetById), new { id = orderId }, orderId);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var order = await _sender.Send(new GetOrderByIdQuery(id), ct);

        if (order is null)
            return NotFound();

        return Ok(order);
    }

    [HttpGet("by-customer/{customerId:guid}")]
    public async Task<IActionResult> GetByCustomer(Guid customerId, CancellationToken ct)
    {
        var orders = await _sender.Send(new GetOrdersByCustomerQuery(customerId), ct);
        return Ok(orders);
    }

    [HttpPost("{id:guid}/items")]
    public async Task<IActionResult> AddItem(Guid id, AddOrderItemRequest request, CancellationToken ct)
    {
        await _sender.Send(new AddOrderItemCommand(
            id, request.ProductId, request.Quantity, request.UnitPrice, request.Currency), ct);
        return NoContent();
    }

    [HttpDelete("{id:guid}/items/{itemId:guid}")]
    public async Task<IActionResult> RemoveItem(Guid id, Guid itemId, CancellationToken ct)
    {
        await _sender.Send(new RemoveOrderItemCommand(id, itemId), ct);
        return NoContent();
    }

    [HttpPut("{id:guid}/pay")]
    public async Task<IActionResult> Pay(Guid id, CancellationToken ct)
    {
        await _sender.Send(new PayOrderCommand(id), ct);
        return NoContent();
    }

    [HttpPut("{id:guid}/ship")]
    public async Task<IActionResult> Ship(Guid id, CancellationToken ct)
    {
        await _sender.Send(new ShipOrderCommand(id), ct);
        return NoContent();
    }

    [HttpPut("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id, CancellationToken ct)
    {
        await _sender.Send(new CompleteOrderCommand(id), ct);
        return NoContent();
    }

    [HttpPut("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken ct)
    {
        await _sender.Send(new CancelOrderCommand(id), ct);
        return NoContent();
    }
}
