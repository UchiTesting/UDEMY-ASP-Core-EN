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

## Binding properties

In the context of coding the functionality of the CreateBook form, we need to pass an instance of book. In the general context, it would simply be passed to the `OnPost()` method.

```csharp
public async Task<IActionResult> OnPost(Book bookObj)
```
There is a `[BindProperty]` annotation that allows to bind it automatically.

```csharp
[BindProperty]
public Book Book { get; set; }

// ... Any code here ...

public async Task<IActionResult> OnPost()
```

After we add the annotation to the Book property, It is assumed `OnPost()` getting this property.

## Validation

Just like ASP.Net MVC, server side validation occurs when you call `ModelState.IsValid`.

Then the display of error messages on the actual page must be added for every single input.

This goes in 2 major steps:
- Enabling validation
- Adding validation for each input

Respectively:
```html
<div class="text-danger" asp-validation-summary="ModelOnly"></div>
```
Possible values for the `asp-validation-summary` tag helper are:

| Value | Description |
|---|---|
| All | Displays both the validation for the model and properties. |
| ModelOnly | Displays a validation text next for the model only. |
| None | No message is displayed. |

```html
<span asp-validation-for="Book.Title" class="text-danger"></span>
```
Simple `asp-validation-for` tag helper is used along with the proper value to display the messages.

To enable client-side validation, we simply need to add a reference to the available validation partial.

At the bottom on the page we add

```html
@section Scripts{
    <partial name="_ValidationScriptsPartial" />
}
```

## Executing a handler

The creation and edition are straightforward operations which redirect to a specific page. Deletion will not redirect to another page yet need some code to be run.

```html
<a asp-page="EditBook" asp-route-id="@item.Id" class="btn btn-success btn-sm text-white">Edit</a>
```

`asp-route-<ParamName>` is usable in a similar way of HTML data attributes. You can replace the last part with an identifier.

`asp-page` tells what is the target page after a POST request.

```html
<button asp-page-handler="Delete" asp-route-id="@item.Id" onclick="return confirm('Are you sure you want to delete @item.Title?')" class="btn btn-danger btn-sm">Delete</button>
```

`asp-page-handler` allows us to determine which handler would be called. Along with the previous tag helper, this is kind of equivalent to ASP.Net MVC Controller/Action routing (see the [official doc @MSDN](https://docs.microsoft.com/fr-fr/aspnet/core/mvc/views/tag-helpers/built-in/anchor-tag-helper?view=aspnetcore-5.0#asp-page-handler)). 

The above `asp-page-handler="Delete"` means there is a `On<Verb>Delete` handler in Index.cshtml.cs where `<Verb>` can be any valid HTTP method such as Get or Post. Because we use a `<button>`, the verb will be Post. We also provide an `id` via `asp-route-id` tag helper. Hence the full target handler name will become `OnPostDelete(int id)`.
