---
name: update-nugets
description: Обновляет NuGet-зависимости шаблона Calabonga.AspNetCore.AppDefinitions.Template (вложенный проект Calabonga.AppDefinitions.Web и сам проект-упаковщик), проверяет шаблон end-to-end (сборка, установка, создание проекта, GET /weatherforecast = 200), делает инкремент Patch в PackageVersion и выводит отчёт "было-стало". Используй, когда просят "обнови nuget-пакеты шаблона", "update-nugets", "актуализируй зависимости" в репозитории Calabonga.AspNetCore.AppDefinitions.Template.
---

# update-nugets

Операция считается успешной, только если выполнены ВСЕ шаги по порядку. При ошибке на любом шаге — остановись, сообщи и не переходи дальше (откатывать версии не нужно, просто доложи).

Корень репозитория: `C:\Projects\Calabonga.AspNetCore.AppDefinitions.Template`. Пути ниже — от него.

- Вложенный проект: `src/Calabonga.AspNetCore.AppDefinitions.Template/content/Calabonga.AppDefinitions.Web/Calabonga.AppDefinitions.Web.csproj`
- Упаковщик: `src/Calabonga.AspNetCore.AppDefinitions.Template/Calabonga.AspNetCore.AppDefinitions.Template.csproj`
- Решение: `src/Calabonga.AspNetCore.AppDefinitions.Template.slnx`

## Шаги

0. Рабочий процесс: убедись, что дерево чистое, создай ветку от актуального `main` (`feature/update-nugets`; если ветка существует — спроси, можно ли использовать). Зафиксируй исходные версии (для отчёта).
1. Зависимости `Calabonga.AppDefinitions.Web`: `dotnet list <csproj> package --outdated`; обнови `Version` в `PackageReference` через правку `.csproj` (sed/Edit). Обновляй только в пределах мажорной версии, совпадающей с TFM (для net10.0 — `10.x`); у сторонних пакетов (Scalar, Serilog и т.п.) — до последней стабильной. Prerelease не бери.
2. Зависимости упаковщика: `dotnet list <csproj> package --outdated`. Версия `Microsoft.TemplateEngine.Tasks` задана как `*` — не трогай; если обновлять нечего, так и отметь в отчёте.
3. Собери оба проекта: `dotnet build <вложенный csproj> -c Release` и `dotnet build <slnx> -c Release`. Обе сборки без ошибок (предупреждения NU1903 о уязвимостях — зафиксируй в отчёте).
4. Упакуй и установи шаблон: `dotnet pack <slnx> -c Release -o $TEMP/appdef-test/nupkg`, затем `dotnet new install $TEMP/appdef-test/nupkg/*.nupkg`. Если шаблон уже был установлен — `install` заменит его (учти при шаге 7).
5. Создай тестовый проект во временной папке: `cd $TEMP/appdef-test && dotnet new webapi-appdef -n Test.Api` (из-за `preferNameDirectory` проект окажется в `Test.Api/Test.Api`). Проверь, что в его `.csproj` версии пакетов — новые.
6. Запусти `dotnet run --no-launch-profile --urls http://localhost:5199` в фоне из папки проекта, дождись готовности и выполни `curl -s -o /dev/null -w "%{http_code}" http://localhost:5199/weatherforecast` минимум 3 раза. Все ответы должны быть `200`. Затем останови приложение (`taskkill //F //IM Test.Api.exe`).
7. Удали шаблон: `dotnet new uninstall Calabonga.AspNetCore.AppDefinitions.Template` (при необходимости повтори, пока не удалены все версии), удали `$TEMP/appdef-test`. Проверь, что `git status` показывает только ожидаемые изменения (`bin/obj` игнорируются).
8. Инкремент Patch в `<PackageVersion>` упаковщика (`10.0.1` → `10.0.2`). Если менялся только вложенный проект — всё равно инкрементируй, иначе CI (`--skip-duplicate`) не опубликует пакет.
9. Выведи отчёт в виде таблиц:

   | Проект | Пакет | Было | Стало |
   |---|---|---|---|

   Добавь строку для `PackageVersion`, результаты шагов 3–7 (сборка, 3×GET = 200) и предупреждения, правку README (шаг 10).
10. Обнови `README.md` (корень репозитория): добавь над самым свежим разделом истории версий новый раздел `## Версия <новый PackageVersion>` с маркированным списком обновлённых пакетов (`имя: было → стало`). Формат и язык (русский) — как у соседних разделов. Выполняй до коммита; в отчёт шага 9 добавь строку про README.

После успеха закоммить изменения (`build: update nuget packages`, README — отдельным коммитом `docs: update README`, атомарно, `Co-Authored-By` по правилам сессии). Пуш и PR — только по просьбе пользователя.

## Что важно знать

- Версия пакета шаблона должна совпадать с мажорной версией NET; не меняй мажорную часть.
- Не публикуй пакет (`dotnet nuget push`) — это делает CI при пуше в `main`.
- Шаблон мог быть установлен у пользователя раньше: `dotnet new uninstall` удаляет все установленные версии — сообщи об этом в отчёте.
