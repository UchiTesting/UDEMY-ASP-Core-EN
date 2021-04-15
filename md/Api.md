Web API
=======

After we made a CRUD for the book, we want to implement an API.
The course goes with an MVC controller. I will attempt to implement is with a rigorous Web API instead. Both methods shall be documented.

## Preparation

The will use additional CSS and JS scripts to provide useful functionalities.

We add the following in the `_Layout.cshtml` file

```html
CSS in header:
<link rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.css" />

JS before the closing body tag and @RenderSection :
<script src="https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
<script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
```

> ASP.Net MVC had a nice bundling feature I don't know so far in ASP.Net Core. Look into it later and replace that note.

## Using an MVC controller (course way)

In `Startup.cs` add:
- `services.AddControllersWithViews();` to the `Configure` method.
- `endpoints.MapControllers();` to `app.UseEndpoints` into the `Configure()` method.

> MVC Controller needs more config than WebAPI to properly display JSON because Unicode characters are not displayed properly by default. French caracters were escaped while OK without further config on Web API.

```csharp
services.AddControllersWithViews() // MVC Controller
   .AddJsonOptions(option => option.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All))
   .AddJsonOptions(option => option.JsonSerializerOptions.WriteIndented = true)
   .AddJsonOptions(option => option.JsonSerializerOptions.IgnoreNullValues = true)
   .AddJsonOptions(option => option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
```

Create a `Controllers` folder in which the controllers will be made available.

Create an MVC Controller in that folder.

Create GET actions

```
[HttpGet]
public IActionResult GetAll() { // ... Do your thing here ... }
```

>Unlike ASP.NET MVC WebAPI, the return type is not `IHttpActionResult` but `IActionResult`.

> Also MVC controllers allow the usage of some methods such as `Json()` that return a `JsonResult` which are not allowed in WebApi version even-though the return type `IActionResult` is the same. The reason is MVC controller derives `Controller` while Web API derives `ControllerBase`

## Using a .Net Core Web API controller (alternate way)

### Initial config

In `Startup.cs` add:
- `services.AddControllers();` to the `Configure()` method.
- `endpoints.MapControllers();` to `app.UseEndpoints` into the `Configure()` method.

> Note: The above is sufficient to enable WebAPI. The default output is JSON. But it can be configured further for convenience and rigorous respect of naming between C# and JSON objects.

> Having both `services.AddControllers()` and `services.AddControllersWithViews()` in `Startup.cs` is mostly duplicate. One or the other is enough to enable API endpoints.

Replace `services.AddControllers();` with

```csharp
services.AddControllers()
            .AddJsonOptions(option => option.JsonSerializerOptions.WriteIndented = true)
            .AddJsonOptions(option => option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
```

> Note : `JsonNamingPolicy` is in `System.Text.Json`.

> Note: You can also pick some of the MVC config above if needed.

Create a `Controllers\api` folder in which the controllers will be made available.

Create a WebAPI controller in that folder.

Create GET actions.

```csharp
[HttpGet]
public IActionResult GetAll()
{
   var list = _db.Books.ToList();

   if (list.Count < 1) return NotFound();

   return Ok(list);
}

[HttpGet("{id}")]
public IActionResult Get(int id)
{
   var book = _db.Books.Find(id);

   if (book is null) return NotFound();

   return Ok(book);
}
```