# Killing Machine

Aplicación web para gimnasio desarrollada con ASP.NET Core MVC, Entity Framework Core y SQLite.

## Requisitos

Instalar:

- .NET SDK 8.0 o superior.
- Git.
- Visual Studio 2022 o Visual Studio Code.

Comprobar la instalación de .NET:

```bash
dotnet --version
```

## Descargar el proyecto

```bash
git clone https://github.com/apitx/KillingMachine.git
cd KillingMachine
```

## Restaurar dependencias

```bash
dotnet restore
```

## Instalar Entity Framework Core CLI

Ejecutar este comando solamente si `dotnet ef` no está instalado:

```bash
dotnet tool install --global dotnet-ef
```

Comprobar la instalación:

```bash
dotnet ef --version
```

## Crear o actualizar la base de datos

```bash
dotnet ef database update
```

La aplicación utiliza SQLite y creará la base de datos local automáticamente.

## Compilar el proyecto

```bash
dotnet build
```

## Ejecutar la aplicación

```bash
dotnet run
```

Abrir en el navegador la dirección mostrada en la terminal. Normalmente:

```text
http://localhost:5080
```

## Ejecutar con recarga automática

```bash
dotnet watch run
```

## Detener la aplicación

Presionar:

```text
Ctrl + C
```

## Limpiar y volver a compilar

```bash
dotnet clean
rm -rf bin obj
dotnet restore
dotnet build
dotnet run
```

En Windows PowerShell, para eliminar `bin` y `obj`:

```powershell
Remove-Item -Recurse -Force bin, obj -ErrorAction SilentlyContinue
dotnet restore
dotnet build
dotnet run
```

## Error por archivo bloqueado

Si aparece un error indicando que `KillingMachine.exe` está siendo utilizado por otro proceso, detener la aplicación desde Visual Studio Code con:

```text
Shift + F5
```

O ejecutar en PowerShell:

```powershell
Get-Process KillingMachine,dotnet -ErrorAction SilentlyContinue | Stop-Process -Force
```

Después:

```bash
dotnet clean
dotnet build
dotnet run
```
