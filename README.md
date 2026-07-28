# Killing Machine
Sitio web del gimnasio Killing Machine.

## Requisitos

- .NET SDK 8.0 o superior.
- Git.
- Visual Studio 2022 o Visual Studio Code.

Comprobar la instalación:

```bash
dotnet --version
```

## Descargar y ejecutar

```bash
git clone https://github.com/apitx/KillingMachine.git
cd KillingMachine
dotnet restore
dotnet tool install --global dotnet-ef
dotnet ef database update
dotnet run
```

Abrir la dirección indicada en la terminal. Normalmente:

```text
http://localhost:5080
```

## Ejecutar con recarga automática

```bash
dotnet watch run
```

## Detener

```text
Ctrl + C
```

## Limpiar y volver a ejecutar

### Git Bash

```bash
rm -rf bin obj
dotnet restore
dotnet build
dotnet run
```

### PowerShell

```powershell
Remove-Item -Recurse -Force bin, obj -ErrorAction SilentlyContinue
dotnet restore
dotnet build
dotnet run
```

## Publicar

```bash
rm -rf publish
dotnet publish KillingMachine.csproj -c Release -o publish
```

Enlace público:

```text
https://killingmachine.runasp.net/
```
