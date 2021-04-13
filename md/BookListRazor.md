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

### Install packages
Install NuGet Packages:
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFramworkCore.Tools`

> See [installed dependencies](PackageDependencies.md).

### Define the connection string to DB

Edit `appsettings.json` to add the connection strings.

```js
{
   "ConnectionStrings": {
       "DefaultConnection": "Server=(LocalDB)\\MSSQLLocalDB;Database=BookListRazor;Trusted_Connection=True;MultipleActiveResultSets=True"
     },
   /* ... remainder of the settings ... */
}
```

### Create EF DbContext class

```csharp
public class AppDbContext : DbContext
{
   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
   
   public DbSet<Book> Books { get; set; }
}
```

The constructor, yet empty is needed as shown above for the needs of dependency injection.

### Enabling EF in ASP.Net Core

```csharp
public void ConfigureServices(IServiceCollection services)
{
   services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

   // ... remainder of the config ...
}
```