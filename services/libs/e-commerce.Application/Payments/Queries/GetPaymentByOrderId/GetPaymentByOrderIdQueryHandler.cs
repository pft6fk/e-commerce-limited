using e_commerce.Application.Interfaces;
using e_commerce.Domain.ValueObjects;
using MediatR;

namespace e_commerce.Application.Payments.Queries.GetPaymentByOrderId;

public class GetPaymentByOrderIdQueryHandler : IRequestHandler<GetPaymentByOrderIdQuery, PaymentResponse?>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetPaymentByOrderIdQueryHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentResponse?> Handle(GetPaymentByOrderIdQuery request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByOrderIdAsync(new OrderId(request.OrderId), cancellationToken);

        if (payment is null)
            return null;

        return new PaymentResponse(
            payment.Id.Value,
            payment.OrderId.Value,
            payment.Amount.Amount,
            payment.Amount.Currency,
            payment.Status.ToString(),
            payment.ProcessedAt);
    }
}
