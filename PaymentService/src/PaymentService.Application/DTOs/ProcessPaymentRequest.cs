namespace PaymentService.Application.DTOs;

public record ProcessPaymentRequest(Guid OrderId, decimal Amount);
