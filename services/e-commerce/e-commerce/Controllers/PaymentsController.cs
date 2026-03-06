using e_commerce.Api.Contracts;
using e_commerce.Application.Payments.Commands.CreatePayment;
using e_commerce.Application.Payments.Commands.ProcessPayment;
using e_commerce.Application.Payments.Queries.GetPaymentByOrderId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly ISender _sender;

    public PaymentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePaymentRequest request, CancellationToken ct)
    {
        var paymentId = await _sender.Send(
            new CreatePaymentCommand(request.OrderId, request.Amount, request.Currency), ct);

        return CreatedAtAction(nameof(GetByOrderId), new { orderId = request.OrderId }, paymentId);
    }

    [HttpGet("by-order/{orderId:guid}")]
    public async Task<IActionResult> GetByOrderId(Guid orderId, CancellationToken ct)
    {
        var payment = await _sender.Send(new GetPaymentByOrderIdQuery(orderId), ct);

        if (payment is null)
            return NotFound();

        return Ok(payment);
    }

    [HttpPut("{id:guid}/process")]
    public async Task<IActionResult> Process(Guid id, CancellationToken ct)
    {
        await _sender.Send(new ProcessPaymentCommand(id), ct);
        return NoContent();
    }
}
