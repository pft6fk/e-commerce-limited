namespace e_commerce.Application.Orders;

public record OrderResponse(
    Guid Id,
    Guid CustomerId,
    string Status,
    decimal TotalPrice,
    string Currency,
    DateTime CreatedAt,
    List<OrderItemResponse> Items);

public record OrderItemResponse(
    Guid Id,
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    string Currency);
