using ECommerce.Application.DTOs.Orders;
using ECommerce.Application.DTOs.Payments;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.Orders.Commands;

public record PayOrderCommand(Guid OrderId, Guid UserId) : IRequest<PayOrderResponse?>;

public class PayOrderHandler : IRequestHandler<PayOrderCommand, PayOrderResponse?>
{
    private readonly IOrderRepository _orders;
    private readonly IPaymentClient   _paymentClient;
    private readonly IUnitOfWork      _uow;

    public PayOrderHandler(IOrderRepository orders, IPaymentClient paymentClient, IUnitOfWork uow)
    {
        _orders        = orders;
        _paymentClient = paymentClient;
        _uow           = uow;
    }

    public async Task<PayOrderResponse?> Handle(PayOrderCommand cmd, CancellationToken ct)
    {
        var order = await _orders.GetByIdWithItemsAsync(cmd.OrderId, ct);

        // No existe o no pertenece al usuario autenticado.
        if (order is null || order.UserId != cmd.UserId) return null;

        // Si el servicio de pagos no responde, acá se lanza PaymentServiceUnavailableException
        // y la orden queda intacta (no se toca el estado ni se guarda nada).
        var payment = await _paymentClient.ProcessAsync(new PaymentRequest(order.Id, order.Total), ct);

        // La regla de transición vive en la entidad, no acá.
        if (string.Equals(payment.Status, "Approved", StringComparison.OrdinalIgnoreCase))
            order.MarkAsPaid();
        else
            order.MarkPaymentRejected();

        await _uow.SaveChangesAsync(ct);

        return new PayOrderResponse(
            order.Id,
            order.Status.ToString(),
            payment.Status,
            payment.TransactionId);
    }
}
