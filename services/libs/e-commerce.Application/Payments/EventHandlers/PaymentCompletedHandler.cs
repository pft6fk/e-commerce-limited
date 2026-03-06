using e_commerce.Application.Common;
using e_commerce.Application.Interfaces;
using e_commerce.Domain.Common;
using e_commerce.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace e_commerce.Application.Payments.EventHandlers;

public class PaymentCompletedHandler : INotificationHandler<DomainEventNotification<PaymentCompletedDomainEvent>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PaymentCompletedHandler> _logger;

    public PaymentCompletedHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        ILogger<PaymentCompletedHandler> logger)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(DomainEventNotification<PaymentCompletedDomainEvent> notification, CancellationToken ct)
    {
        var orderId = notification.DomainEvent.OrderId;

        var order = await _orderRepository.GetByIdAsync(orderId, ct)
            ?? throw new DomainException($"Order {orderId.Value} not found.");

        order.Pay();
        await _unitOfWork.SaveChangesAsync(ct);

        _logger.LogInformation("Order {OrderId} marked as paid after payment {PaymentId} completed",
            orderId.Value, notification.DomainEvent.PaymentId.Value);
    }
}
