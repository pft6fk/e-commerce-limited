using MediatR;

namespace e_commerce.Application.Stock.Queries.GetStockByProductId;

public record GetStockByProductIdQuery(Guid ProductId) : IRequest<StockItemResponse?>;
