namespace ECommerce.Application.Exceptions;

// Se lanza cuando el PaymentService no responde (caído o timeout).
// El middleware de la WebApi la mapea a 503 Service Unavailable.
public class PaymentServiceUnavailableException : Exception
{
    public PaymentServiceUnavailableException(string message) : base(message) { }
    public PaymentServiceUnavailableException(string message, Exception innerException)
        : base(message, innerException) { }
}
