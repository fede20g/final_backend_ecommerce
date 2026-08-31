using PaymentService.Domain.Enums;
using PaymentService.Domain.Exceptions;

namespace PaymentService.Domain.Entities;

public class Payment : BaseEntity
{
    // Límite de aprobación: se aprueba el pago si el monto es menor a este valor.
    public const decimal ApprovalLimit = 100000m;

    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public string TransactionId { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    private Payment() { }   // EF Core

    private Payment(Guid orderId, decimal amount, PaymentStatus status)
    {
        OrderId       = orderId;
        Amount        = amount;
        Status        = status;
        TransactionId = $"TX-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
        CreatedAt     = DateTime.UtcNow;
    }

    // Regla de negocio: aprueba si el monto es menor al límite, si no lo rechaza.
    public static Payment Process(Guid orderId, decimal amount)
    {
        if (orderId == Guid.Empty)
            throw new DomainException("El identificador de la orden es obligatorio.");
        if (amount <= 0)
            throw new DomainException("El monto debe ser mayor a cero.");

        var status = amount < ApprovalLimit ? PaymentStatus.Approved : PaymentStatus.Rejected;
        return new Payment(orderId, amount, status);
    }
}
