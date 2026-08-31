using System.Net.Http.Json;
using ECommerce.Application.DTOs.Payments;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interfaces;

namespace ECommerce.Infrastructure.Services;

// Cliente HTTP tipado hacia el PaymentService.
// Traduce los fallos de transporte (caída / timeout) en una excepción propia,
// para que la capa Application no dependa de detalles de HTTP.
public class PaymentClient : IPaymentClient
{
    private readonly HttpClient _http;

    public PaymentClient(HttpClient http) => _http = http;

    public async Task<PaymentResponse> ProcessAsync(PaymentRequest request, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("/api/payments/process", request, ct);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PaymentResponse>(cancellationToken: ct);
            if (result is null)
                throw new PaymentServiceUnavailableException(
                    "El servicio de pagos devolvió una respuesta vacía.");

            return result;
        }
        catch (HttpRequestException ex)   // servicio caído, error de red o status no exitoso
        {
            throw new PaymentServiceUnavailableException(
                "El servicio de pagos no está disponible. La orden queda confirmada; reintentá el pago más tarde.", ex);
        }
        catch (TaskCanceledException ex) when (!ct.IsCancellationRequested)   // timeout
        {
            throw new PaymentServiceUnavailableException(
                "El servicio de pagos tardó demasiado en responder. La orden queda confirmada; reintentá el pago más tarde.", ex);
        }
    }
}
