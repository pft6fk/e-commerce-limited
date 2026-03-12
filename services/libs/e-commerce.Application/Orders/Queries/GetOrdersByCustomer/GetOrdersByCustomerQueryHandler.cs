using e_commerce.Application.Interfaces;
using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;
using MediatR;

namespace e_commerce.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerQueryHandler : IRequestHandler<GetOrdersByCustomerQuery, List<OrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public GetOrdersByCustomerQueryHandler(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<List<OrderResponse>> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetByCustomerIdAsync(new CustomerId(request.CustomerId), cancellationToken);

        var productIds = orders.SelectMany(o => o.Items).Select(i => i.ProductId).Distinct().ToList();

        var productTasks = productIds.Select(id => _productRepository.GetByIdAsync(id, cancellationToken));
        var productArray = await Task.WhenAll(productTasks);
        var products = productArray.Where(p => p != null).ToDictionary(p => p.Id.Value, p => p.Name);

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
                products.GetValueOrDefault(i.ProductId.Value, "Unknown Product"),
                i.Quantity,
                i.UnitPrice.Amount,
                i.UnitPrice.Currency)).ToList())).ToList();
    }
}
