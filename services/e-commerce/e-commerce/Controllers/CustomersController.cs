using e_commerce.Api.Contracts;
using e_commerce.Application.Customers.Commands.RegisterCustomer;
using e_commerce.Application.Customers.Queries.GetAllCustomers;
using e_commerce.Application.Customers.Queries.GetCustomerById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ISender _sender;

    public CustomersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var customers = await _sender.Send(new GetAllCustomersQuery(), ct);
        return Ok(customers);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterCustomerRequest request, CancellationToken ct)
    {
        var customerId = await _sender.Send(
            new RegisterCustomerCommand(request.FirstName, request.LastName, request.Email), ct);

        return CreatedAtAction(nameof(GetById), new { id = customerId }, customerId);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var customer = await _sender.Send(new GetCustomerByIdQuery(id), ct);

        if (customer is null)
            return NotFound();

        return Ok(customer);
    }
}
