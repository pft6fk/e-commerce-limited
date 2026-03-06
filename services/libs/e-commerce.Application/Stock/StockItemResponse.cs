namespace e_commerce.Application.Stock;

public record StockItemResponse(
    Guid ProductId,
    int AvailableQuantity);
