# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Обзор

Репозиторий содержит NuGet-пакет `Calabonga.AspNetCore.AppDefinitions.Template` — шаблон для `dotnet new` (`shortName`: `webapi-appdef`), создающий ASP.NET Core Web API на NET10 с использованием библиотеки `Calabonga.AspNetCore.AppDefinitions`. Библиотека позволяет разложить настройку приложения из `Program.cs` по классам-определениям (`AppDefinition`). Собственного исполняемого кода в репозитории нет: весь код лежит в `content/` как исходник шаблона. Версия пакета (`PackageVersion`) совпадает с версией платформы NET (сейчас `10.0.0`). README написан на русском языке и содержит историю версий.

## Команды

```bash
dotnet restore src/Calabonga.AspNetCore.AppDefinitions.Template.slnx
dotnet pack src/Calabonga.AspNetCore.AppDefinitions.Template.slnx --configuration Release --output ./nupkg   # сборка NuGet-пакета (как в CI)

dotnet new install ./nupkg/Calabonga.AspNetCore.AppDefinitions.Template.10.0.0.nupkg   # локальная установка шаблона для проверки
dotnet new webapi-appdef -n My.Api                                                       # создать проект из шаблона
dotnet new uninstall Calabonga.AspNetCore.AppDefinitions.Template
```

Тестов и линтеров в репозитории нет. Проект шаблона в `content/` напрямую в решение не входит, но его можно собрать отдельно: `dotnet build src/Calabonga.AspNetCore.AppDefinitions.Template/content/Calabonga.AppDefinitions.Web/Calabonga.AppDefinitions.Web.csproj`.

## Архитектура

- `src/Calabonga.AspNetCore.AppDefinitions.Template.slnx` — решение (формат slnx), содержит единственный проект-упаковщик.
- `src/Calabonga.AspNetCore.AppDefinitions.Template/*.csproj` — проект-упаковщик: `PackageType=Template`, `IncludeBuildOutput=false`, `Compile Remove="**\*"`; в пакет включается всё из `content\**` (кроме `bin`, `obj`, `.vs`). Также упаковываются `README.md` и `logo.png`.
- `content/.template.config/template.json` — манифест шаблона: `sourceName` = `Calabonga.AppDefinitions.Web` (заменяется на имя нового проекта), параметр `Framework` (выбор TFM, сейчас только `net10.0`, заменяет строку `net10.0` в файлах).
- `content/Calabonga.AppDefinitions.Web/` — исходник создаваемого проекта:
  - `Program.cs` — `builder.AddDefinitions(typeof(Program))` и `app.UseDefinitions()`, плюс Serilog (настройка из конфигурации) и обработка `HostAbortedException`.
  - `Definitions/CommonDefinition.cs` — `AppDefinition` с `AddOpenApi`, в Development — `MapOpenApi` и Scalar UI (`MapScalarApiReference`), `UseHttpsRedirection`.
  - `Endpoints/WeatherForecastEndpoints.cs` — пример `AppDefinition` с minimal API эндпоинтом `/weatherforecast`.
  - `Entities/WeatherForecast.cs`, `appsettings*.json`, `Properties/launchSettings.json`.
- CI: `.github/workflows/package.yml` — при push в `main` (или вручную) выполняет restore → `dotnet pack` (Release) → `dotnet nuget push` на nuget.org с `--skip-duplicate` и секретом `NUGET_API_KEY`.

### Что важно знать

- Публикация происходит при каждом push в `main`; из-за `--skip-duplicate` изменения без повышения `PackageVersion` в `.csproj` на nuget.org не попадут. Версию поднимай вместе с записью в `README.md` (раздел истории версий) и `PackageReleaseNotes`.
- При смене версии NET нужно синхронно обновить: `TargetFramework` в упаковщике и в `Calabonga.AppDefinitions.Web.csproj`, `choices`/`defaultValue` и `description` в `template.json`, имя workflow, `PackageVersion`, версии пакетов в `Calabonga.AppDefinitions.Web.csproj`.
- `Framework` в `template.json` заменяет текст `net10.0` во всех файлах шаблона — не используй эту строку в содержимом `content/` в иных смыслах.
- Строка `Calabonga.AppDefinitions.Web` — `sourceName`: она заменяется в именах файлов и namespace при создании проекта, поэтому все namespace и имена в `content/` должны использовать именно её.
- `Compile Remove="**\*"` в упаковщике нужен, чтобы `.cs` файлы из `content/` не компилировались в проект-упаковщик; не удаляй.
- Версии NuGet-зависимостей шаблона указаны в `content/.../Calabonga.AppDefinitions.Web.csproj`, а не в упаковщике (там только `Microsoft.TemplateEngine.Tasks`).
