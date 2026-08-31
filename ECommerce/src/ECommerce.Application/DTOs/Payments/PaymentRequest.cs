namespace ECommerce.Application.DTOs.Payments;

// Contrato de salida hacia el PaymentService.
// Es un DTO propio del e-commerce: los servicios no comparten código.
public record PaymentRequest(Guid OrderId, decimal Amount);
