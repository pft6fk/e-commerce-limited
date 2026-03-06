using MediatR;

namespace e_commerce.Application.Products.Commands.ActivateProduct;

public record ActivateProductCommand(Guid ProductId) : IRequest;
