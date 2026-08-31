namespace ECommerce.Domain.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(Guid id)
        : base($"Usuario con Id '{id}' no encontrado.") { }
}
