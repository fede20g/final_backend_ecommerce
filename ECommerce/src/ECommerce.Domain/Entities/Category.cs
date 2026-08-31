using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    private Category() { }  // para EF Core

    public Category(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("El nombre de la categoría es obligatorio.");

        Name = name;
    }
}
