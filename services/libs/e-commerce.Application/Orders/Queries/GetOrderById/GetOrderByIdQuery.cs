using MediatR;

namespace e_commerce.Application.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid Id) : IRequest<OrderResponse?>;
