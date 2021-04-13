Book list Razor
===============

## Runtime compilation

Install the NuGet package `Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation`.

> See [installed dependencies](PackageDependencies.md).

In `Startup.cs` in the `ConfigureServices()`  method, there is a line where Razor has been enabled. Chain the `AddRazorRuntimeCompilation()` method.

```csharp
services.AddRazorPages().AddRazorRuntimeCompilation();
```

It allows refreshing the page to see the modified view, without the need to recompile.

## Setup DB

Install NuGet Packages:
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFramworkCore.Tools`

> See [installed dependencies](PackageDependencies.md).

Edit `appsettings.json` to add the connection strings.

```js
{
   "ConnectionStrings": {
       "DefaultConnection": "Server=(LocalDB)\\MSSQLLocalDB;Database=BookListRazor;Trusted_Connection=True;MultipleActiveResultSets=True"
     },
   /* ... remainder of the file */
}
```
