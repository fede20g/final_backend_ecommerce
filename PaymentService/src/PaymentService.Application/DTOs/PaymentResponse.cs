namespace PaymentService.Application.DTOs;

public record PaymentResponse(
    Guid Id,
    Guid OrderId,
    decimal Amount,
    string Status,
    string TransactionId,
    DateTime CreatedAt
);
