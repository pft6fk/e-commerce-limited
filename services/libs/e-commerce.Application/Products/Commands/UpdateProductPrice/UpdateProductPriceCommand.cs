using MediatR;

namespace e_commerce.Application.Products.Commands.UpdateProductPrice;

public record UpdateProductPriceCommand(Guid ProductId, decimal Price, string Currency) : IRequest;
