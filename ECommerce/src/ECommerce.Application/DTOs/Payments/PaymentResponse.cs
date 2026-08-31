namespace ECommerce.Application.DTOs.Payments;

// Contrato de entrada desde el PaymentService.
// Solo declaramos los campos que necesitamos; el resto se ignora al deserializar.
public record PaymentResponse(string Status, string TransactionId);
