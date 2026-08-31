# PaymentService

Segundo servicio del sistema distribuido del **Trabajo Final Integrador** (Backend — Optativa II). Simula la aprobación o el rechazo de pagos para el e-commerce. Es un proyecto .NET **independiente**, con su propia solución y puerto, y se comunica con el e-commerce únicamente por HTTP.

> El sistema completo son dos servicios: este (`PaymentService/`) y el **e-commerce** (`ECommerce/`). El flujo end-to-end y cómo levantar ambos está documentado en el README del e-commerce, sección **Sistema distribuido**.

---

## Tecnologías

| Tecnología | Uso |
|---|---|
| .NET 8 / ASP.NET Core | Framework principal |
| Entity Framework Core 8.0.11 | ORM y migraciones |
| SQLite | Base de datos (`payments.db`) |
| MediatR 12 | CQRS — Commands, Queries y Handlers |
| Swagger / OpenAPI | Documentación interactiva |

---

## Arquitectura

Misma Clean Architecture que el e-commerce, con cuatro capas:

```
src/
├── PaymentService.Domain/          # Entidad Payment, regla de negocio, excepciones
├── PaymentService.Application/     # Command, Query, Handlers, DTOs
├── PaymentService.Infrastructure/  # DbContext, repositorio, EF Core
└── PaymentService.Api/             # Controller, startup, configuración
```

La regla de dependencia apunta hacia adentro: `Domain` no depende de nada; `Application` solo de `Domain`; `Infrastructure` y `Api` de las capas internas.

---

## Regla de negocio

El servicio **aprueba** el pago si el monto es **menor a 100000**; si es mayor o igual, lo **rechaza**. La decisión vive en el dominio (`Payment.Process`) y el límite es la constante `Payment.ApprovalLimit`. Cada intento genera un `transactionId` y se persiste.

---

## Endpoints

| Método | Ruta | Descripción |
|---|---|---|
| POST | `/api/payments/process` | Procesa un pago. Recibe `{ orderId, amount }`, devuelve `{ status, transactionId }` |
| GET | `/api/payments/order/{orderId}` | Lista los pagos registrados para una orden |

Ejemplo de comunicación:

```jsonc
// Request
POST /api/payments/process
{ "orderId": "a1b2...", "amount": 45000 }

// Response (monto < 100000)
{ "status": "Approved", "transactionId": "TX-9F3C1A2B" }

// Response (monto >= 100000)
{ "status": "Rejected", "transactionId": "TX-4D7E0C11" }
```

---

## Ejecución

```powershell
cd PaymentService
dotnet run --project src/PaymentService.Api
```

Disponible en `http://localhost:5100`; Swagger en `http://localhost:5100/swagger`. Al arrancar aplica las migraciones automáticamente y crea `payments.db` si no existe.

### Migraciones

```powershell
# Crear la migración inicial (ya incluida en el repo)
dotnet ef migrations add InitialCreate --project src/PaymentService.Infrastructure --startup-project src/PaymentService.Api
```

---

## Persistencia

Los pagos se guardan en la tabla `Payments` (SQLite, EF Core). El `Status` se persiste como texto (`Approved` / `Rejected`).

---

## Seguridad

Este servicio **no requiere autenticación**: la llamada entre servicios va sin token (decisión documentada). El JWT y los roles se mantienen en el e-commerce, sobre sus propios endpoints.
