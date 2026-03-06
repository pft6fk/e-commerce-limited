using e_commerce.Application.Interfaces;
using e_commerce.Domain.ValueObjects;
using MediatR;

namespace e_commerce.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerQueryHandler : IRequestHandler<GetOrdersByCustomerQuery, List<OrderResponse>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersByCustomerQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<OrderResponse>> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetByCustomerIdAsync(new CustomerId(request.CustomerId), cancellationToken);

        return orders.Select(order => new OrderResponse(
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
                i.UnitPrice.Currency)).ToList())).ToList();
    }
}
