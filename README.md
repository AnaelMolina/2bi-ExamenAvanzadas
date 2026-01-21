# Taller 3 - Arquitectura de Microservicios con API Gateway

## 📋 Descripción
Este proyecto implementa una arquitectura de microservicios con:
- **ApiGateway**: Punto de entrada único que gestiona autenticación JWT y redirige peticiones
- **ApiEmpleado**: Microservicio con CRUD de empleados y protección inteligente

## 🏗️ Arquitectura

```
Cliente (Postman)
      │
      ▼
┌─────────────────────────────────┐
│   API Gateway (Puerto 5003)     │
│   - Autenticación JWT           │
│   - Redirección con Ocelot      │
│   - Header secreto              │
└────────────────┬────────────────┘
                 │
                 ▼
┌─────────────────────────────────┐
│   ApiEmpleado (Puerto 5002)     │
│   - CRUD Empleados              │
│   - Detección automática Gateway│
│   - Protección dinámica         │
└────────────────┬────────────────┘
                 │
                 ▼
┌─────────────────────────────────┐
│   PostgreSQL                    │
│   Base: empleados_db            │
│   Tabla: empleado               │
└─────────────────────────────────┘
```

## 🔧 Requisitos
- .NET 10 SDK
- PostgreSQL 17

## 🗄️ Configuración de Base de Datos

```sql
CREATE DATABASE empleados_db;

CREATE TABLE empleado (
    "Id" SERIAL PRIMARY KEY,
    "Cedula" VARCHAR(20) NOT NULL,
    "Nombres" VARCHAR(100) NOT NULL,
    "Apellidos" VARCHAR(100) NOT NULL
);
```

## ⚙️ Configuración

### ApiEmpleado/appsettings.json
Actualizar la contraseña de PostgreSQL:
```json
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=empleados_db;Username=postgres;Password=TU_PASSWORD"
}
```

## 🚀 Ejecución

### Terminal 1 - ApiEmpleado
```bash
cd ApiEmpleado
dotnet run
```

### Terminal 2 - ApiGateway
```bash
cd ApiGateway
dotnet run
```

## 🔗 Endpoints

### Autenticación
| Método | URL | Body |
|--------|-----|------|
| POST | `http://localhost:5003/api/auth/login` | `{"usuario":"admin","password":"admin"}` |

### Empleados (requieren token JWT)
| Método | URL | Descripción |
|--------|-----|-------------|
| GET | `http://localhost:5003/api/empleados` | Listar todos |
| GET | `http://localhost:5003/api/empleados/{id}` | Obtener uno |
| POST | `http://localhost:5003/api/empleados` | Crear |
| PUT | `http://localhost:5003/api/empleados/{id}` | Actualizar |
| DELETE | `http://localhost:5003/api/empleados/{id}` | Eliminar |

### Estado del Gateway
| Método | URL | Descripción |
|--------|-----|-------------|
| GET | `http://localhost:5002/api/gateway-status/status` | Ver si Gateway está activo |

## 🔐 Seguridad Implementada

1. **JWT**: Todas las peticiones a `/api/empleados` requieren token Bearer
2. **Header Secreto**: El Gateway agrega `X-Gateway-Secret` a las peticiones
3. **Detección Automática**: ApiEmpleado detecta si el Gateway está activo
   - Gateway activo → Bloquea acceso directo
   - Gateway apagado → Permite acceso directo

## 📝 Ejemplo de uso en Postman

1. **Login**:
   - POST `http://localhost:5003/api/auth/login`
   - Body: `{"usuario":"admin","password":"admin"}`
   - Copiar el token de la respuesta

2. **Crear empleado**:
   - POST `http://localhost:5003/api/empleados`
   - Header: `Authorization: Bearer <tu_token>`
   - Body: `{"cedula":"1234567890","nombres":"Juan","apellidos":"Pérez"}`

## 👨‍💻 Autor
Taller 3 - Arquitectura de Aplicaciones Web
