using MediatR;
using PaymentService.Application.DTOs;
using PaymentService.Domain.Interfaces;

namespace PaymentService.Application.Features.Payments.Queries;

public record GetPaymentByOrderIdQuery(Guid OrderId) : IRequest<List<PaymentResponse>>;

public class GetPaymentByOrderIdHandler : IRequestHandler<GetPaymentByOrderIdQuery, List<PaymentResponse>>
{
    private readonly IPaymentRepository _payments;

    public GetPaymentByOrderIdHandler(IPaymentRepository payments) => _payments = payments;

    public async Task<List<PaymentResponse>> Handle(GetPaymentByOrderIdQuery query, CancellationToken ct)
    {
        var payments = await _payments.GetByOrderIdAsync(query.OrderId, ct);
        return payments
            .Select(p => new PaymentResponse(
                p.Id, p.OrderId, p.Amount, p.Status.ToString(), p.TransactionId, p.CreatedAt))
            .ToList();
    }
}
