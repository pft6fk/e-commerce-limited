using MediatR;

namespace e_commerce.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    string Currency,
    bool IsActive) : IRequest<Guid>;
