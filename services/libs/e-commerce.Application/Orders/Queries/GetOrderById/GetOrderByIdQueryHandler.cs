using e_commerce.Application.Interfaces;
using e_commerce.Domain.ValueObjects;
using MediatR;

namespace e_commerce.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse?>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<OrderResponse?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(new OrderId(request.Id), cancellationToken);

        if (order is null)
            return null;
        var productIds = order.Items.Select(i => i.ProductId).Distinct().ToList();
        var products = productIds.Select(productIds => _productRepository.GetByIdAsync(productIds, cancellationToken));
        var productsDict = (await Task.WhenAll(products)).Where(p => p is not null).ToDictionary(p => p!.Id, p => p!);
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
                productsDict.TryGetValue(i.ProductId, out var product) ? product.Name : "Unknown Product",
                i.Quantity,
                i.UnitPrice.Amount,
                i.UnitPrice.Currency)).ToList());
    }
}
