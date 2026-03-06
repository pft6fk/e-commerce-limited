using MediatR;

namespace e_commerce.Application.Customers.Commands.RegisterCustomer;

public record RegisterCustomerCommand(
    string FirstName,
    string LastName,
    string Email) : IRequest<Guid>;
