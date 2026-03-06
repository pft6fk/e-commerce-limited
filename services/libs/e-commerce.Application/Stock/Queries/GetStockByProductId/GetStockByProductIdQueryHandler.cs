using e_commerce.Application.Interfaces;
using e_commerce.Domain.ValueObjects;
using MediatR;

namespace e_commerce.Application.Stock.Queries.GetStockByProductId;

public class GetStockByProductIdQueryHandler : IRequestHandler<GetStockByProductIdQuery, StockItemResponse?>
{
    private readonly IStockItemRepository _stockItemRepository;

    public GetStockByProductIdQueryHandler(IStockItemRepository stockItemRepository)
    {
        _stockItemRepository = stockItemRepository;
    }

    public async Task<StockItemResponse?> Handle(GetStockByProductIdQuery request, CancellationToken cancellationToken)
    {
        var stock = await _stockItemRepository.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

        if (stock is null)
            return null;

        return new StockItemResponse(
            stock.Id.Value,
            stock.AvailableQuantity);
    }
}
