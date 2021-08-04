# OpenNasa.Apod
Blazor WebAssembly with Azure Functions app for Astronomy Picture of the Day

[![Azure Static Web Apps CI/CD](https://github.com/gmarokov/OpenNasa.Apod/actions/workflows/azure-static-web-apps-wonderful-sea-05fed8c03.yml/badge.svg)](https://github.com/gmarokov/OpenNasa.Apod/actions/workflows/azure-static-web-apps-wonderful-sea-05fed8c03.yml)

## Getting started

### Multiple startup for Blazor app and Function app
- In Visual Studio configure the multiple project startup to run the Client and the Api
- In Visual Studio Code choose "Attach to .NET Functions/Launch and Debug Standalone Blazor WebAssembly App" debug configuration for multi-target debugging
- Or from CLI run `func start` from `src/Api` and then run `dotnet run` from `src/Client`
