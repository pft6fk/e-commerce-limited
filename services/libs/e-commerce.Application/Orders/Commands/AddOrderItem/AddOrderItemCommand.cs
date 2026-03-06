using MediatR;

namespace e_commerce.Application.Orders.Commands.AddOrderItem;

public record AddOrderItemCommand(
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    string Currency) : IRequest;
