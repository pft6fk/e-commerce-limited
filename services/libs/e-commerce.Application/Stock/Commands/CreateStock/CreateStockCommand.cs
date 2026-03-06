using MediatR;

namespace e_commerce.Application.Stock.Commands.CreateStock;

public record CreateStockCommand(Guid ProductId, int Quantity) : IRequest<Guid>;
