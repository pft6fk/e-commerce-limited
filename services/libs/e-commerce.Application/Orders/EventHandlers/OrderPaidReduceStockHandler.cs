using e_commerce.Application.Common;
using e_commerce.Application.Interfaces;
using e_commerce.Domain.Common;
using e_commerce.Domain.Events;
using e_commerce.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace e_commerce.Application.Orders.EventHandlers;

public class OrderPaidReduceStockHandler : INotificationHandler<DomainEventNotification<OrderPaidDomainEvent>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IStockItemRepository _stockRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderPaidReduceStockHandler> _logger;

    public OrderPaidReduceStockHandler(
        IOrderRepository orderRepository,
        IStockItemRepository stockRepository,
        IUnitOfWork unitOfWork,
        ILogger<OrderPaidReduceStockHandler> logger)
    {
        _orderRepository = orderRepository;
        _stockRepository = stockRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(DomainEventNotification<OrderPaidDomainEvent> notification, CancellationToken ct)
    {
        var orderId = notification.DomainEvent.OrderId;

        var order = await _orderRepository.GetByIdAsync(orderId, ct)
            ?? throw new DomainException($"Order {orderId.Value} not found.");

        foreach (var item in order.Items)
        {
            var stock = await _stockRepository.GetByIdAsync(item.ProductId, ct);
            if (stock is null)
            {
                _logger.LogWarning("No stock record found for product {ProductId}, skipping reduction", item.ProductId.Value);
                continue;
            }

            stock.ReduceStock(item.Quantity);
            _logger.LogInformation("Reduced stock for product {ProductId} by {Quantity}", item.ProductId.Value, item.Quantity);
        }

        await _unitOfWork.SaveChangesAsync(ct);
    }
}
