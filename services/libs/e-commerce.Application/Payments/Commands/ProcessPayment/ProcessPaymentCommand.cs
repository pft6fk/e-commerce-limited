using MediatR;

namespace e_commerce.Application.Payments.Commands.ProcessPayment;

public record ProcessPaymentCommand(Guid PaymentId) : IRequest;
