namespace e_commerce.Api.Contracts;

public record CreateStockRequest(Guid ProductId, int Quantity);
