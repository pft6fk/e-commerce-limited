using e_commerce.Application.Interfaces;
using e_commerce.Domain.Common;
using e_commerce.Domain.ValueObjects;
using MediatR;

namespace e_commerce.Application.Orders.Commands.RemoveOrderItem;

public class RemoveOrderItemCommandHandler : IRequestHandler<RemoveOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveOrderItemCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(new OrderId(request.OrderId), cancellationToken)
            ?? throw new DomainException($"Order {request.OrderId} not found.");

        order.RemoveItem(new OrderItemId(request.ItemId));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
