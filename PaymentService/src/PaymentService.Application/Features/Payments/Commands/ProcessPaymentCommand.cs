using MediatR;
using PaymentService.Application.DTOs;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;

namespace PaymentService.Application.Features.Payments.Commands;

public record ProcessPaymentCommand(Guid OrderId, decimal Amount) : IRequest<PaymentResponse>;

public class ProcessPaymentHandler : IRequestHandler<ProcessPaymentCommand, PaymentResponse>
{
    private readonly IPaymentRepository _payments;
    private readonly IUnitOfWork        _uow;

    public ProcessPaymentHandler(IPaymentRepository payments, IUnitOfWork uow)
    {
        _payments = payments;
        _uow      = uow;
    }

    public async Task<PaymentResponse> Handle(ProcessPaymentCommand cmd, CancellationToken ct)
    {
        // La regla de negocio (aprobar/rechazar) vive en el dominio.
        var payment = Payment.Process(cmd.OrderId, cmd.Amount);

        await _payments.AddAsync(payment, ct);
        await _uow.SaveChangesAsync(ct);

        return new PaymentResponse(
            payment.Id, payment.OrderId, payment.Amount,
            payment.Status.ToString(), payment.TransactionId, payment.CreatedAt);
    }
}
