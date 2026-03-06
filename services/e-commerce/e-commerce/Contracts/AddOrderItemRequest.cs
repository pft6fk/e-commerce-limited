namespace e_commerce.Api.Contracts;

public record AddOrderItemRequest(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    string Currency);
