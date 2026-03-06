namespace e_commerce.Application.Customers;

public record CustomerResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime RegisteredAt);
