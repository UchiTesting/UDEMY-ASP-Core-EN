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

## Migrations

Nothing special outside the fact we did not start with `enable-migrations` but directly created a migration with `add-migration`. After checking of the script just performed the usual `update-database` command.

## BookList Razor page

So far we have defined a Book Model and used EF so that it creates the relevant DB and table in SQL Server.

> /BookList/Index.cshtml.cs

```csharp
   public class IndexModel : PageModel
   {
      private AppDbContext _db;

      public IndexModel(AppDbContext db)
      {
         _db = db;
      }

      public IEnumerable<Book> Books { get; set; }
      public async Task OnGet()
      {
         Books = await _db.Books.ToListAsync();
      }
   }
```
Just like in MVC we define a private property for the needs of the upcoming methods.

In the constructor, we take advantage of dependency injection to inject the relevant DbContext as defined in the `Startup.cs` earlier.

We populate a local property `Books` from the methods.
Note that the default return type for `OnGet()` was `void`, but because we are using asynchronous methods to retrieve the data, we replaced it with `async Task`

The view uses HTML along with Razor syntax, HTML helpers and Tag Helpers. See source of `Pages\BookList\Index.cshtml`.