using MediatR;

namespace e_commerce.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(Guid CustomerId) : IRequest<Guid>;
