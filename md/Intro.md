Introduction
============

This will contain quick notes only.

## Razor pages

They are in the Views folder if MVC is enabled and Pages folder otherwise.

## Project specific files

| File | Description |
|---|---|
| csproj | Contains meta information about the project along with specific dependencies. |
| .launchsettings | Contains the launch profiles information that can also be edited from the project properties. |

## Structure

The first thing I noticed when I created my first ASP.Net Core project is at last it looks a bit more like Java EE project i.e. there is a `wwwroot` folder for static resources. A bit of research had me understand RESTful apps are done with similar annotations such as `@Produces` on Java EE and `[Produces]` on ASP.Net Core. That gets me pretty excited to learn more.

There are 2 ways to create ASP.Net Core :
- One which has the MVC approach
- One that replaces controllers and Views with a Pages folder.

In the latter context, each view will be made of:
  - A cshtml file
  - A cshtml.cs file extending `PageModel` which replaces Controllers.  

The views start with a `@page` annotation before the `@model` one.
The cshtml.cs file has methods called `OnGet()` and respectively `OnPost()` and alike for the other HTTP methods. Again it looks closer to Java EE which has `doPost()` or `doGet()`.

## Routing

`Pages` folder is the root folder by default.

| URL | Maps to |
|---|---|
| www.domain.com | /Pages/Index.cshtml |
| www.domain.com/Index | /Pages/Index.cshtml |
| www.domain.com/account | /Pages/account.cshtml |
| www.domain.com/account | /Pages/account/index.cshtml |

## Tag Helpers

Do not confuse them with HTML helpers which are basically methods that output HTML. Tag Helpers are a equivalent to JSTL (	Java server pages Standard Tag Library). They are represented as attributes in the HTML code.

Some equivalents:

| HTML Helper | Tag Helper |
|---|---|
| `@Html.Label("FirstName", "FirstName: ", new {@class = "form-control"})` | `<label class="form-control" asp-for="FirstName">`|
| `@Html.LabelFor( m => m.FirstName, new {@class = "col-md-2 control-label"})` | `<label class="col-md-2 control-label" asp-for="FirstName">`|

## Main method

ASP.Net Core applications has a `Main()` method in the `Program` class just like another C# program.

It amongst other initiate a host configuration ending up in the Startup class.
This class has several methods to configure the application.

```csharp
void CongigureServices(IServiceCollection services)
```
It allows to define services available to the application. The default template has Razor pages configured by default. Services that could be added for instance are EF Core, Identity Service, MVC and more.


```csharp
void Configure(IApplicationBuilder app, IWebHostEnvironment env)
```

Configure the HTTP request pipeline. The `app` param can provide methods to configure middlewares to be used.

## Middleware

They are software components that can be combined to customize the way we ahndle request and response.
The classic scenario is each middleware passes its output to the next until we reach the end of the pipeline. That said, it may happen that at some point a middleware determine the response should be returned as is without going further in the pipeline.