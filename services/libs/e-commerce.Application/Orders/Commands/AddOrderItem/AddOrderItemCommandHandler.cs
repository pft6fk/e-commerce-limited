using e_commerce.Application.Interfaces;
using e_commerce.Domain.Common;
using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;
using MediatR;

namespace e_commerce.Application.Orders.Commands.AddOrderItem;

public class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddOrderItemCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(new OrderId(request.OrderId), cancellationToken)
            ?? throw new DomainException($"Order {request.OrderId} not found.");

        var item = new OrderItem(
            request.Quantity,
            new Money(request.UnitPrice, request.Currency),
            new ProductId(request.ProductId));

        order.AddItem(item);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
