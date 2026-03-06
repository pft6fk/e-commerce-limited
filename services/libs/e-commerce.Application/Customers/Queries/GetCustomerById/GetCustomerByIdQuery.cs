using MediatR;

namespace e_commerce.Application.Customers.Queries.GetCustomerById;

public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerResponse?>;
