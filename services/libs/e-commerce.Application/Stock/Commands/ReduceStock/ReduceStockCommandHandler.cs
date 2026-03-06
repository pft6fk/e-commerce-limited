using e_commerce.Application.Interfaces;
using e_commerce.Domain.Common;
using e_commerce.Domain.ValueObjects;
using MediatR;

namespace e_commerce.Application.Stock.Commands.ReduceStock;

public class ReduceStockCommandHandler : IRequestHandler<ReduceStockCommand>
{
    private readonly IStockItemRepository _stockItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReduceStockCommandHandler(IStockItemRepository stockItemRepository, IUnitOfWork unitOfWork)
    {
        _stockItemRepository = stockItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ReduceStockCommand request, CancellationToken cancellationToken)
    {
        var stock = await _stockItemRepository.GetByIdAsync(new ProductId(request.ProductId), cancellationToken)
            ?? throw new DomainException($"Stock for product {request.ProductId} not found.");

        stock.ReduceStock(request.Quantity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
