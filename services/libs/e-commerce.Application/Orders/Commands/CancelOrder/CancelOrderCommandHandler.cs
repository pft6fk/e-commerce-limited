using e_commerce.Application.Interfaces;
using e_commerce.Domain.Common;
using e_commerce.Domain.ValueObjects;
using MediatR;

namespace e_commerce.Application.Orders.Commands.CancelOrder;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(new OrderId(request.OrderId), cancellationToken)
            ?? throw new DomainException($"Order {request.OrderId} not found.");

        order.Cancel();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
