using e_commerce.Application.Interfaces;
using e_commerce.Domain.ValueObjects;
using MediatR;

namespace e_commerce.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse?>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderResponse?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(new OrderId(request.Id), cancellationToken);

        if (order is null)
            return null;

        return new OrderResponse(
            order.Id.Value,
            order.CustomerId.Value,
            order.Status.ToString(),
            order.TotalPrice.Amount,
            order.TotalPrice.Currency,
            order.CreatedAt,
            order.Items.Select(i => new OrderItemResponse(
                i.Id.Value,
                i.ProductId.Value,
                i.Quantity,
                i.UnitPrice.Amount,
                i.UnitPrice.Currency)).ToList());
    }
}
