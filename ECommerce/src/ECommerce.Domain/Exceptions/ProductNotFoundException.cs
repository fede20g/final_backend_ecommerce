namespace ECommerce.Domain.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid id)
    : base($"Producto con Id '{id}' no encontrado.") { }
}