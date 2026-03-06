using e_commerce.Application.Interfaces;
using e_commerce.Domain.ValueObjects;
using MediatR;

namespace e_commerce.Application.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerResponse?>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerResponse?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.Id), cancellationToken);

        if (customer is null)
            return null;

        return new CustomerResponse(
            customer.Id.Value,
            customer.FirstName,
            customer.LastName,
            customer.Email,
            customer.RegisteredAt);
    }
}
