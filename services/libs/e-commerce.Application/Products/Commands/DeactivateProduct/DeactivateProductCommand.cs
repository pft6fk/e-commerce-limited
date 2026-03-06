using MediatR;

namespace e_commerce.Application.Products.Commands.DeactivateProduct;

public record DeactivateProductCommand(Guid ProductId) : IRequest;
