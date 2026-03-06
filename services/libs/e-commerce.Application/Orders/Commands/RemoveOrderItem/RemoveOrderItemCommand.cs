using MediatR;

namespace e_commerce.Application.Orders.Commands.RemoveOrderItem;

public record RemoveOrderItemCommand(Guid OrderId, Guid ItemId) : IRequest;
