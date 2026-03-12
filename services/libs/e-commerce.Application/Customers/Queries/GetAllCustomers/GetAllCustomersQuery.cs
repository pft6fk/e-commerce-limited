using MediatR;

namespace e_commerce.Application.Customers.Queries.GetAllCustomers;

public record GetAllCustomersQuery : IRequest<List<CustomerResponse>>;
