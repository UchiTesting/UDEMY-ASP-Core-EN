Introduction
============

This will contain quick notes only.

## Razor pages

They are in the Views folder. According to the course you have `cshtml` and nested `cshtml.cs` files that would be holding the page model and therefore extending `PageModel`. It does not seem to be the case anymore. I don't have these files and did not work with Razor that way on the ASP.Net MVC projects I've done so far.

## Project specific files

| File | Description |
|---|---|
| csproj | Contains meta information about the project along with specific dependencies. |
| .launchsettings | Contains the launch profiles information that can also be edited from the project properties. |

## Structure

***The project structure is not the same as in the course*** overall which means adaptation and research to get to a similar result. I notice there is no Controller in the course version in which they must have been replaced by those `cshtml.cs` files.

The first thing I noticed when I created my first ASP.Net Core project is at last it looks a bit more like Java EE project i.e. there is a `wwwroot` folder for static resources. A bit of research had me understand RESTful apps are done with similar annotations such as `@Produces` on Java EE and `[Produces]` on ASP.Net Core. That gets me pretty excited to learn more.

Other than that, the structure seems to be very close to ASP.Net MVC with `Controllers` and `Models`