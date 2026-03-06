using MediatR;

namespace e_commerce.Application.Stock.Commands.IncreaseStock;

public record IncreaseStockCommand(Guid ProductId, int Quantity) : IRequest;
