# Blazored Menu

A JavaScript free menu library for Blazor and Razor Components applications.

[![Build Status](https://dev.azure.com/blazored/Menu/_apis/build/status/Blazored.Menu?branchName=master)](https://dev.azure.com/blazored/Menu/_build/latest?definitionId=7&branchName=master)

[![Nuget](https://img.shields.io/nuget/v/blazored.menu.svg)](https://www.nuget.org/packages/Blazored.Menu/)

## Important Notice For ASP.NET Core Razor Components Apps
There is currently an issue with [ASP.NET Core Razor Components apps](https://devblogs.microsoft.com/aspnet/aspnet-core-3-preview-2/#sharing-component-libraries) (not Blazor). They are unable to import static assets from component libraries such as this one. 

You can still use this package, however, you will need to manually add the JavaScript file to your Razor Components `wwwroot` folder. Then you will need to reference it in your `index.html`.

Alternatively, there is a great package by [Mister Magoo](https://github.com/SQL-MisterMagoo/BlazorEmbedLibrary) which offers a solution to this problem without having to manually copy files.

## Getting Setup
You can install the package via the nuget package manager just search for *Blazored.Menu*. You can also install via powershell using the following command.

```powershell
Install-Package Blazored.Menu
```

Or via the dotnet CLI.

```bash
dotnet add package Blazored.Menu
```

### Add Imports
Add the following to your *_ViewImports.cshtml*

```csharp
@using Blazored.Menu

@addTagHelper *, Blazored.Menu
```

## Usage
Blazored Menu allows menus to be built either using markup or dynamically, using the `MenuBuilder`.

### Building a menu with markup
You can build your menus using the following components.

- BlazoredMenu
- BlazoredMenuItem
- BlazoredSubMenu

For example.

```html
<BlazoredMenu>
  <BlazoredMenuItem>
    <NavLink href="/" Match="NavLinkMatch.All">Home</NavLink>
  </BlazoredMenuItem>
  <BlazoredSubMenu Header="Sub Menu">
        <MenuTemplate>
            <BlazoredMenuItem>
                <NavLink href="counter">Counter</NavLink>
            </BlazoredMenuItem>
        </MenuTemplate>
    </BlazoredSubMenu>
    <BlazoredMenuItem>
        <NavLink href="fetchdata">Fetch data</NavLink>
    </BlazoredMenuItem>
</BlazoredMenu>
```

You can also specify your own template for sub menu headers like so.

```html
<BlazoredMenu>
  <BlazoredMenuItem>
    <NavLink href="/" Match="NavLinkMatch.All">Home</NavLink>
  </BlazoredMenuItem>
  <BlazoredSubMenu Header="Sub Menu">
        <HeaderTemplate>
            <i class="my-cool-icon-class"></i> Sub Menu
        </HeaderTemplate>
        <MenuTemplate>
            <BlazoredMenuItem>
                <NavLink href="counter">Counter</NavLink>
            </BlazoredMenuItem>
            <BlazoredMenuItem>
                <NavLink href="fetchdata">Fetch data</NavLink>
            </BlazoredMenuItem>
        </MenuTemplate>
    </BlazoredSubMenu>
</BlazoredMenu>
```

_This feature is currently only available when building menus with markup._

You can also supply your own CSS classes to each of the 3 components
- BlazoredMenu
- BlazoredMenuItem
- BlazoredSubMenu

By setting the `Css` parameter.

```html
<BlazoredMenu Css="custom-menu-css">
  <BlazoredMenuItem Css="custom-menuitem-css">
    <NavLink href="/" Match="NavLinkMatch.All">Home</NavLink>
  </BlazoredMenuItem>
  <BlazoredSubMenu Header="Sub Menu" Css="custom-submenu-css">
        <HeaderTemplate>
            <i class="my-cool-icon-class"></i> Sub Menu
        </HeaderTemplate>
        <MenuTemplate>
            <BlazoredMenuItem>
                <NavLink href="counter">Counter</NavLink>
            </BlazoredMenuItem>
            <BlazoredMenuItem>
                <NavLink href="fetchdata">Fetch data</NavLink>
            </BlazoredMenuItem>
        </MenuTemplate>
    </BlazoredSubMenu>
</BlazoredMenu>
```


### Building a menu dynamically using the MenuBuilder
If you prefer you can use the `MenuBuilder` to create your menus using C#. The `MenuBuilder` exposes two methods `AddItem` and `AddSubMenu`. You can build the same menu from the markup example as follows.

```html
<BlazoredMenu MenuBuilder="@MenuBuilder" />

@functions {
    MenuBuilder MenuBuilder = new MenuBuilder();

    protected override void OnInit()
    {
        MenuBuilder.AddItem(1, "Home", "/")
                   .AddSubMenu(2, "Sub Menu", new MenuBuilder().AddItem(1, "Counter", "counter")
                   .AddItem(3, "FetchData", "fetchdata");
    }
}
```

### MenuBuilder Options
When using the `MenuBuilder` you have a couple of extra options available via the `AddItem` and `AddSubMenu` methods. 

- IsEnabled (default: true)
- IsVisible (default: true)

You can use these options to manipulate your menu items. `IsVisible`, if set to `false`, will mark your menu items as `display: none` making them invisible. Setting `IsEnabled` to `false` will render the item in a non-interactive state.







