# Trabajo Final Integrador — Backend (Optativa II)

Sistema distribuido simple: un **e-commerce** (servicio principal) que se comunica por **HTTP** con un segundo servicio independiente, el **PaymentService**, que simula la aprobación o el rechazo de pagos. Son dos proyectos .NET 8 separados, con la misma Clean Architecture, comunicados únicamente por HTTP.

> **Opción elegida:** Opción 1 — *PaymentService*.

## Estructura del repositorio

Este es **un solo repositorio con dos carpetas**, un proyecto .NET independiente en cada una:

| Carpeta | Servicio | Puerto | Rol |
|---|---|---|---|
| [`ECommerce/`](ECommerce/) | E-commerce | `http://localhost:5000` | Servicio principal y **cliente HTTP** |
| [`PaymentService/`](PaymentService/) | PaymentService | `http://localhost:5100` | Segundo servicio, **procesa pagos** |

Cada carpeta tiene su propia solución (`.sln`) y su README con el detalle:
- **[README del E-commerce](ECommerce/README.md)** — arquitectura, endpoints, sistema distribuido, base de datos.
- **[README del PaymentService](PaymentService/README.md)** — regla de negocio, endpoints, persistencia.

## Regla de negocio del segundo servicio

El PaymentService **aprueba** el pago si el monto es **menor a 100000**; si es mayor o igual, lo **rechaza**. La regla vive en el dominio (`Payment.Process`) y el límite es la constante `Payment.ApprovalLimit`.

## Cómo levantar los dos servicios

Cada uno en su propia terminal. Levantar **primero el PaymentService**, así está disponible cuando el e-commerce lo llame:

```powershell
# Terminal 1 — PaymentService (segundo servicio)
cd PaymentService
dotnet run --project src/PaymentService.Api      # http://localhost:5100

# Terminal 2 — E-commerce (servicio principal)
cd ECommerce
dotnet run --project src/ECommerce.WebApi         # http://localhost:5000
```

El flujo completo se reproduce desde el Swagger del e-commerce en `http://localhost:5000/swagger`.

## Flujo end-to-end de una orden

1. `POST /api/orders` → crea la orden (estado `Pending`).
2. `POST /api/orders/{id}/confirm` → `Confirmed`.
3. `POST /api/orders/{id}/pay` → el e-commerce llama al PaymentService con `{ orderId, amount }`. Según responda `Approved` o `Rejected`, la orden queda en `Paid` o `PaymentRejected`.

Si el PaymentService está caído o tarda más de 10 s, la API responde **503** y la orden queda intacta en `Confirmed` (el pago se puede reintentar).

## Usuario Admin de prueba

La base incluye un administrador precargado por migración (solo para desarrollo local):

| Email | Contraseña | Rol |
|---|---|---|
| `admin@ecommerce.com` | `Admin1234!` | `Admin` |

## Stack

.NET 8 · ASP.NET Core · Entity Framework Core 8.0.11 (SQLite) · MediatR 12 (CQRS) · JWT Bearer + BCrypt (e-commerce) · Swagger. Clean Architecture de cuatro capas en ambos proyectos.
