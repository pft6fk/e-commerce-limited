using e_commerce.Application.Interfaces;
using MediatR;

namespace e_commerce.Application.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<List<CustomerResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync(cancellationToken);

        return customers.Select(c => new CustomerResponse(
            c.Id.Value,
            c.FirstName,
            c.LastName,
            c.Email,
            c.RegisteredAt)).ToList();
    }
}
