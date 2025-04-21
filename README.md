# ASP.NET Core Web API with Application Definitions

Calabonga.AspNetCore.AppDefinitions.Template использует сборку `Calabonga.AspNetCore.AppDefinitions` ([nuget](https://github.com/Calabonga/Calabonga.AspNetCore.AppDefinitions)), которая позволяет навести порядок в вашем Program.cs. Можно всё разложить "по полочкам". А еще можно реализовать систему плагинов. Чтобы воспользоваться сборкой надо просто установить nuget-пакет [Calabonga.AspNetCore.AppDefinitions](https://www.nuget.org/packages/Calabonga.AspNetCore.AppDefinitions/).

Define your Web API application parts in different definitions (files). It's remarkably simple and useful. You will know what features your application consists of.

# Версия 5.0.0

* NET8 удалена из сборки.
* `OpenApi` added 
* `Swagger` removed
* `Scalar` API UI added for testing in DEGUB mode

## Версия 4.0.0

* Сборка переведена на NET9.
* Добавлена возможность выбора версии NET.

## Версия 3.0.0
* Сборка переведена на NET8 по причине скорого окончания срока действия NET6.
* Добавлены описание методов (summaries).
* Изменены описание для ясности и однозначности.
* Исправлены некоторые синтаксические ошибки.

## Версия 2.4.3
* Генерирование Open API спецификации по умолчанию отключено (commented)
* Исправлены синтаксические ошибки
* Обновлены nuget-пакеты

## Версия 2.4.2
* Обновлено строение папок в проекте.
* Удалена ненужная SolutionItems папка.

## Версия 2.4.1

* Обновлены nuget-пакеты, в частности, обновлен `AppDefinition` пакет, где был добавлен перехват ошибок при поиске модулей определений (AppDefinition).
* Удалена ненужная зависимость `MassTransit.RabbitMQ`

## Версия 2.4.0

* Nuget-пакеты обновлены для использования `NET8`
* Правка в документации по установке

## Версия 2.3.0

* Nuget-пакеты обновлены
* Правка в документации по установке

## Как установить (How to install template)

Чтобы установить шаблон для `ASP.NET Core Web API with AppDefinitions` достаточно выполнить команду:

``` bash
dotnet new install Calabonga.AspNetCore.AppDefinitions.Template
```

Кстати, эта же команда обновит шаблон, если появилась новая версия. (By the way, this command will update template if the new version exists.)

![install appdef](whatnot/images/install-appdef.png)

Проверить установлены ли шаблоны можно командой 
``` bash
dotnet new list
```

![installed appdef](whatnot/images/template-appdef-installed.png)

Команда выведет список всех установленных шаблонов. Также после установки шаблонов, они станут доступы в Visual Studio и JetBrains Rider. 

![installed appdef](whatnot/images/template-appdef-installed.png)

Команда выведет список всех установленных шаблонов. Также после установки шаблонов, они станут доступы в Visual Studio и JetBrains Rider. 

## Как удалить (How to uninstall template)
Чтобы удалить шаблон Web API с установленными AppDefinitions достаточно выполнить команду:

``` bash
dotnet new uninstall Calabonga.AspNetCore.AppDefinitions.Template
```

![uninstall appdef](whatnot/images/uninstall-appdef.png)

## Как использовать (How to use)

При создании нового проекта, можно выбрать новый шаблон в Visual Studio

![visual-studio-template](whatnot/images/new-project-create.png)

А также можно выбрать новый шаблон в JetBrains Rider

![rider-template](whatnot/images/new-project-create2.png)

## Больше информации (More info)

* [Application Definitions](https://www.calabonga.net/blog/post/devtool-21-application-definitions) - Статья как использовать nuget-пакет.
* [NuGet Calabonga.AspNetCore.AppDefinitions](https://www.nuget.org/packages/Calabonga.AspNetCore.AppDefinitions/)
* [Github repository](https://github.com/Calabonga/Calabonga.AspNetCore.AppDefinitions)
