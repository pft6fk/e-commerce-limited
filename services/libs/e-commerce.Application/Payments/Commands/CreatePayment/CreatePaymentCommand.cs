using MediatR;

namespace e_commerce.Application.Payments.Commands.CreatePayment;

public record CreatePaymentCommand(
    Guid OrderId,
    decimal Amount,
    string Currency) : IRequest<Guid>;
