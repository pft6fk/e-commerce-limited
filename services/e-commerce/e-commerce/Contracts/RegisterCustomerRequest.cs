namespace e_commerce.Api.Contracts;

public record RegisterCustomerRequest(
    string FirstName,
    string LastName,
    string Email);
