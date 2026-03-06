using MediatR;

namespace e_commerce.Application.Orders.Commands.PayOrder;

public record PayOrderCommand(Guid OrderId) : IRequest;
