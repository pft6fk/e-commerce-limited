using MediatR;

namespace e_commerce.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerQuery(Guid CustomerId) : IRequest<List<OrderResponse>>;
