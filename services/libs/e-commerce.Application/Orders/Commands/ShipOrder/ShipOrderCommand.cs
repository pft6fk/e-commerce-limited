using MediatR;

namespace e_commerce.Application.Orders.Commands.ShipOrder;

public record ShipOrderCommand(Guid OrderId) : IRequest;
