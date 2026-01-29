# Microservices Architecture - Docker Setup

Este proyecto contiene 3 microservicios orquestados con Docker Compose.

## 🐳 Servicios

1. **API Gateway** (Puerto 5003)
   - Enrutador central que dirige las peticiones a los microservicios
   - URL: `http://localhost:5003`

2. **Discovery Service - Consul** (Puerto 8500)
   - Servicio de descubrimiento de servicios
   - UI: `http://localhost:8500`

3. **API Vehiculo** (Puerto 5004)
   - Gestiona vehículos (ID, Placa, Provincia)
   - URL: `http://localhost:5004/api/vehiculos`

4. **API Cliente** (Puerto 5005)
   - Gestiona clientes (ID, Cedula, Nombre, Telefono)
   - URL: `http://localhost:5005/api/clientes`

5. **PostgreSQL** (Puerto 5432)
   - Base de datos
   - Host: `postgres`
   - Usuario: `postgres`
   - Contraseña: `postgres`

## 🚀 Cómo levantar los servicios

### Requisitos previos
- Docker instalado
- Docker Compose instalado

### Pasos

1. **Navegate al directorio del proyecto:**
   ```bash
   cd c:\Users\Anael\examenDOs\2bi-ExamenAvanzadas
   ```

2. **Levanta todos los servicios:**
   ```bash
   docker-compose up -d
   ```

3. **Verifica que los servicios estén corriendo:**
   ```bash
   docker-compose ps
   ```

## 🔍 Acceder a los servicios

- **API Gateway**: http://localhost:5003
- **Consul UI**: http://localhost:8500
- **API Vehiculo directo**: http://localhost:5004/api/vehiculos
- **API Cliente directo**: http://localhost:5005/api/clientes

## 📝 Ejemplos de peticiones

### A través del Gateway (Recomendado)

```bash
# Obtener todos los vehículos
curl http://localhost:5003/api/vehiculos

# Obtener todos los clientes
curl http://localhost:5003/api/clientes

# Crear un vehículo
curl -X POST http://localhost:5003/api/vehiculos \
  -H "Content-Type: application/json" \
  -d '{"placa":"ABC-1234","provincia":"San José"}'

# Crear un cliente
curl -X POST http://localhost:5003/api/clientes \
  -H "Content-Type: application/json" \
  -d '{"cedula":"1234567890","nombre":"Juan","telefono":"2234-5678"}'
```

## 🛑 Detener los servicios

```bash
docker-compose down
```

## 🗑️ Limpiar todo (incluyendo volúmenes)

```bash
docker-compose down -v
```

## 📋 Logs de servicios

```bash
# Ver logs de todos los servicios
docker-compose logs -f

# Ver logs de un servicio específico
docker-compose logs -f api-gateway
docker-compose logs -f api-vehiculo
docker-compose logs -f api-cliente
```

## 🔧 Reconstruir imágenes

```bash
docker-compose build
docker-compose up -d
```
