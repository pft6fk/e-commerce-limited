using e_commerce.Application.Interfaces;
using e_commerce.Domain.Common;
using e_commerce.Domain.Models;
using MediatR;

namespace e_commerce.Application.Customers.Commands.RegisterCustomer;

public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var existing = await _customerRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existing is not null)
            throw new DomainException($"Customer with email {request.Email} already exists.");

        var customer = new Customer(request.FirstName, request.LastName, request.Email);

        await _customerRepository.AddAsync(customer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return customer.Id.Value;
    }
}
