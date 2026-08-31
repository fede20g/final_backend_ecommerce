using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PaymentService.Infrastructure.Persistence;

// Permite que las herramientas de EF (dotnet ef migrations) creen el DbContext
// en tiempo de diseño, sin necesidad de levantar la aplicación.
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("Data Source=payments.db")
            .Options;

        return new ApplicationDbContext(options);
    }
}
