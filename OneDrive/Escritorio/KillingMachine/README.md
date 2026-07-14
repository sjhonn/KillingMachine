# Killing Machine

Aplicacion web publica para un gimnasio, desarrollada con ASP.NET Core MVC, Entity Framework Core y SQLite. Incluye sitio institucional, formularios, dashboard, calculadora IMC y CRUD completo para todos los modulos de datos.

## Tecnologias

- ASP.NET Core MVC sobre .NET 8
- C#
- Entity Framework Core 8
- SQLite local
- HTML, CSS y JavaScript sin librerias externas
- Graficos Canvas sin API ni servicio de pago

## Modulos con CRUD completo

1. Clientes
2. Membresias asignadas a clientes
3. Entrenadores
4. Servicios
5. Planes comerciales
6. Ejercicios
7. Planes de entrenamiento
8. Entrenamientos realizados
9. Peso y medidas corporales
10. Calendario semanal
11. Galeria
12. Mensajes de contacto
13. Solicitudes de clase de prueba

Cada modulo permite crear, listar, consultar, editar y eliminar registros. No se implemento autenticacion porque el requerimiento establece acceso para todo el publico.

## Funciones principales

- Pagina de inicio responsive
- Logo en cabecera, portada y favicon
- Servicios, membresias, entrenadores y galeria
- Formulario de contacto
- Solicitud de clase de prueba
- Boton directo de WhatsApp
- Registro de clientes
- Registro de ejercicios y entrenamientos
- Control de peso, medidas e IMC
- Calendario semanal
- Dashboard con indicadores y graficos de progreso
- Validaciones mediante Data Annotations y reglas adicionales
- Datos de demostracion cargados automaticamente
- Base de datos local sin costos

## Requisitos

- .NET SDK 8.0
- Visual Studio 2022 con la carga de trabajo ASP.NET y desarrollo web, o Visual Studio Code con la extension C# Dev Kit

Compruebe la instalacion:

```bash
dotnet --version
```

## Ejecucion rapida

Abra una terminal dentro de la carpeta del proyecto y ejecute:

```bash
dotnet restore
dotnet run
```

La aplicacion crea y actualiza automaticamente `killingmachine.db` mediante la migracion incluida. Abra la direccion mostrada en la terminal, normalmente:

```text
http://localhost:5080
```

## Ejecucion con migraciones manuales

Instale la herramienta si todavia no existe:

```bash
dotnet tool install --global dotnet-ef
```

Luego ejecute:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

## Reiniciar la base de datos

Detenga la aplicacion, elimine los archivos siguientes y vuelva a ejecutar `dotnet run`:

```text
killingmachine.db
killingmachine.db-shm
killingmachine.db-wal
```

La migracion y los datos iniciales se cargaran nuevamente.

## Abrir en Visual Studio

1. Abra `KillingMachine.sln`.
2. Espere la restauracion de paquetes NuGet.
3. Seleccione el perfil `https` o `http`.
4. Presione `F5`.

## Abrir en Visual Studio Code

1. Abra la carpeta `KillingMachine`.
2. Ejecute `dotnet restore`.
3. Presione `F5` o ejecute `dotnet run`.

La carpeta `.vscode` contiene configuracion de compilacion y depuracion.

## Configuracion

La conexion SQLite se encuentra en `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=killingmachine.db"
}
```

El telefono de WhatsApp, direccion, correo y horarios se pueden modificar en:

```text
Views/Shared/_Layout.cshtml
Views/Home/Index.cshtml
```

## Estructura

```text
Controllers/   Controladores MVC y operaciones CRUD
Data/          DbContext y datos iniciales
Migrations/    Migracion inicial SQLite
Models/        Entidades y validaciones
ViewModels/    Modelos para inicio y dashboard
Views/         Paginas Razor MVC
wwwroot/       CSS, JavaScript e imagenes
```

## Consideracion para produccion

El panel CRUD es publico por requerimiento. En un sistema real se recomienda proteger la gestion de clientes, pagos, mensajes y medidas corporales mediante ASP.NET Core Identity, roles, HTTPS, copias de seguridad y una politica formal de privacidad.
