using MediatR;

namespace e_commerce.Application.Stock.Commands.ReduceStock;

public record ReduceStockCommand(Guid ProductId, int Quantity) : IRequest;
