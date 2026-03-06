using e_commerce.Application.Interfaces;
using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;
using MediatR;

namespace e_commerce.Application.Stock.Commands.CreateStock;

public class CreateStockCommandHandler : IRequestHandler<CreateStockCommand, Guid>
{
    private readonly IStockItemRepository _stockItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStockCommandHandler(IStockItemRepository stockItemRepository, IUnitOfWork unitOfWork)
    {
        _stockItemRepository = stockItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateStockCommand request, CancellationToken cancellationToken)
    {
        var productId = new ProductId(request.ProductId);

        var existing = await _stockItemRepository.GetByIdAsync(productId, cancellationToken);
        if (existing is not null)
            throw new Domain.Common.DomainException($"Stock for product {request.ProductId} already exists.");

        var stock = new StockItem(productId, request.Quantity);

        await _stockItemRepository.AddAsync(stock, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return stock.Id.Value;
    }
}
