using ECommerce.Application.DTOs.Payments;

namespace ECommerce.Application.Interfaces;

// Puerto: la Application declara QUÉ necesita.
// La implementación (HttpClient) vive en Infrastructure.
public interface IPaymentClient
{
    Task<PaymentResponse> ProcessAsync(PaymentRequest request, CancellationToken ct = default);
}
