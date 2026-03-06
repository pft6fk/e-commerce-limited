namespace e_commerce.Api.Contracts;

public record CreatePaymentRequest(
    Guid OrderId,
    decimal Amount,
    string Currency);
