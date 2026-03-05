namespace e_commerce.Api.Contracts;

public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string Currency,
    bool IsActive);
