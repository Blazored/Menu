# Blazored Menu

A JavaScript free menu library for Blazor and Razor Components applications.

[![Build Status](https://dev.azure.com/blazored/Menu/_apis/build/status/Blazored.Menu?branchName=master)](https://dev.azure.com/blazored/Menu/_build/latest?definitionId=7&branchName=master)

## Important Notice For ASP.NET Core Razor Components Apps
There is currently an issue with [ASP.NET Core Razor Components apps](https://devblogs.microsoft.com/aspnet/aspnet-core-3-preview-2/#sharing-component-libraries) (not Blazor). They are unable to import static assets from component libraries such as this one. 

You can still use this package, however, you will need to manually add the JavaScript file to your Razor Components `wwwroot` folder. Then you will need to reference it in your `index.html`.

Alternatively, there is a great package by [Mister Magoo](https://github.com/SQL-MisterMagoo/BlazorEmbedLibrary) which offers a solution to this problem without having to manually copy files.
