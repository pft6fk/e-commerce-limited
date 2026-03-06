using MediatR;

namespace e_commerce.Application.Orders.Commands.CompleteOrder;

public record CompleteOrderCommand(Guid OrderId) : IRequest;
