using e_commerce.Api.Contracts;
using e_commerce.Application.Products.Commands.ActivateProduct;
using e_commerce.Application.Products.Commands.CreateProduct;
using e_commerce.Application.Products.Commands.DeactivateProduct;
using e_commerce.Application.Products.Commands.UpdateProductPrice;
using e_commerce.Application.Products.Queries.GetAllProducts;
using e_commerce.Application.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ISender _sender;

    public ProductsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request, CancellationToken ct)
    {
        var command = new CreateProductCommand(
            request.Name,
            request.Description,
            request.Price,
            request.Currency,
            request.IsActive);

        var productId = await _sender.Send(command, ct);

        return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var product = await _sender.Send(new GetProductByIdQuery(id), ct);

        if (product is null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var products = await _sender.Send(new GetAllProductsQuery(), ct);
        return Ok(products);
    }

    [HttpPut("{id:guid}/price")]
    public async Task<IActionResult> UpdatePrice(Guid id, UpdateProductPriceRequest request, CancellationToken ct)
    {
        await _sender.Send(new UpdateProductPriceCommand(id, request.Price, request.Currency), ct);
        return NoContent();
    }

    [HttpPut("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id, CancellationToken ct)
    {
        await _sender.Send(new ActivateProductCommand(id), ct);
        return NoContent();
    }

    [HttpPut("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken ct)
    {
        await _sender.Send(new DeactivateProductCommand(id), ct);
        return NoContent();
    }
}
