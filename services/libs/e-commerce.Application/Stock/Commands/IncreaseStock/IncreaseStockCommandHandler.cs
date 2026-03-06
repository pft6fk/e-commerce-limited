using e_commerce.Application.Interfaces;
using e_commerce.Domain.Common;
using e_commerce.Domain.ValueObjects;
using MediatR;

namespace e_commerce.Application.Stock.Commands.IncreaseStock;

public class IncreaseStockCommandHandler : IRequestHandler<IncreaseStockCommand>
{
    private readonly IStockItemRepository _stockItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public IncreaseStockCommandHandler(IStockItemRepository stockItemRepository, IUnitOfWork unitOfWork)
    {
        _stockItemRepository = stockItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(IncreaseStockCommand request, CancellationToken cancellationToken)
    {
        var stock = await _stockItemRepository.GetByIdAsync(new ProductId(request.ProductId), cancellationToken)
            ?? throw new DomainException($"Stock for product {request.ProductId} not found.");

        stock.IncreaseStock(request.Quantity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
