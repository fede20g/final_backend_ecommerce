using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PaymentService.Application;
using PaymentService.Domain.Exceptions;
using PaymentService.Infrastructure;
using PaymentService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Crea/actualiza payments.db aplicando las migraciones pendientes al arrancar.
using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

app.UseExceptionHandler(errApp =>
    errApp.Run(async ctx =>
    {
        var ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
        ctx.Response.ContentType = "application/json";
        ctx.Response.StatusCode  = ex is DomainException ? 400 : 500;
        await ctx.Response.WriteAsJsonAsync(new { error = ex?.Message ?? "Error interno." });
    }));

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
