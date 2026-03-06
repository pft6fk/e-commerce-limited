namespace e_commerce.Application.Payments;

public record PaymentResponse(
    Guid Id,
    Guid OrderId,
    decimal Amount,
    string Currency,
    string Status,
    DateTime ProcessedAt);
