using PaymentService.Domain.Entities;

namespace PaymentService.Domain.Interfaces;

public interface IPaymentRepository
{
    Task AddAsync(Payment payment, CancellationToken ct = default);
    Task<IEnumerable<Payment>> GetByOrderIdAsync(Guid orderId, CancellationToken ct = default);
}
