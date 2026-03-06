using e_commerce.Api.Contracts;
using e_commerce.Application.Stock.Commands.IncreaseStock;
using e_commerce.Application.Stock.Commands.ReduceStock;
using e_commerce.Application.Stock.Queries.GetStockByProductId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly ISender _sender;

    public StockController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("by-product/{productId:guid}")]
    public async Task<IActionResult> GetByProductId(Guid productId, CancellationToken ct)
    {
        var stock = await _sender.Send(new GetStockByProductIdQuery(productId), ct);

        if (stock is null)
            return NotFound();

        return Ok(stock);
    }

    [HttpPut("by-product/{productId:guid}/increase")]
    public async Task<IActionResult> Increase(Guid productId, UpdateStockRequest request, CancellationToken ct)
    {
        await _sender.Send(new IncreaseStockCommand(productId, request.Quantity), ct);
        return NoContent();
    }

    [HttpPut("by-product/{productId:guid}/reduce")]
    public async Task<IActionResult> Reduce(Guid productId, UpdateStockRequest request, CancellationToken ct)
    {
        await _sender.Send(new ReduceStockCommand(productId, request.Quantity), ct);
        return NoContent();
    }
}
