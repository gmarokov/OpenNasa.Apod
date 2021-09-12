# OpenNasa.Apod
Blazor WebAssembly with Azure Functions app for Astronomy Picture of the Day powered by [NASA Open API](https://api.nasa.gov/).

[![Azure Static Web Apps CI/CD](https://github.com/gmarokov/OpenNasa.Apod/actions/workflows/azure-static-web-apps-wonderful-sea-05fed8c03.yml/badge.svg)](https://github.com/gmarokov/OpenNasa.Apod/actions/workflows/azure-static-web-apps-wonderful-sea-05fed8c03.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=gmarokov_OpenNasa.Apod&metric=alert_status)](https://sonarcloud.io/dashboard?id=gmarokov_OpenNasa.Apod)
[![Live site](https://img.shields.io/website?label=Website&url=https://wonderful-sea-05fed8c03.azurestaticapps.net//)](https://wonderful-sea-05fed8c03.azurestaticapps.net//)

![Example view](./example.jpg)
## Getting started
0. Checkout the repository
1. Download Azure Functions CLI tools
2. Make sure Storage Emulator is started (its part of the .NET SDK)

### Multiple startup for Blazor app and Function app
- In Visual Studio configure the multiple project startup to run the Client and the Api
- In Visual Studio Code choose "Launch Functions and Debug Blazor WebAssembly App" debug configuration for multi-target debugging
- Or from CLI run `func start` from `src/Api` and then run `dotnet run` from `src/Client`
