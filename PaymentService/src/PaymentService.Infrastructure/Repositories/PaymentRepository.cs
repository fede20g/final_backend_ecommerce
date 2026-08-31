using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;
using PaymentService.Infrastructure.Persistence;

namespace PaymentService.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _ctx;
    public PaymentRepository(ApplicationDbContext ctx) => _ctx = ctx;

    public async Task AddAsync(Payment payment, CancellationToken ct = default)
        => await _ctx.Payments.AddAsync(payment, ct);

    public async Task<IEnumerable<Payment>> GetByOrderIdAsync(Guid orderId, CancellationToken ct = default)
        => await _ctx.Payments
               .AsNoTracking()
               .Where(p => p.OrderId == orderId)
               .OrderByDescending(p => p.CreatedAt)
               .ToListAsync(ct);
}
