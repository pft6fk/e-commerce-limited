namespace e_commerce.Api.Contracts;

public record UpdateProductPriceRequest(
    decimal Price,
    string Currency);
