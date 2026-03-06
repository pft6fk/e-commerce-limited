using MediatR;

namespace e_commerce.Application.Payments.Queries.GetPaymentByOrderId;

public record GetPaymentByOrderIdQuery(Guid OrderId) : IRequest<PaymentResponse?>;
