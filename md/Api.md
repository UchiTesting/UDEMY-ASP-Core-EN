Web API
=======

After we made a CRUD for the book, we want to implement an API.
The course goes with an MVC controller. I will attempt to implement is with a rigorous Web API instead. Both methods shall be documented.

## Using an MVC controller (course way)

## Using a .Net Core Web API controller (alternate way)

### Initial config

In `Startup.cs` add:
- `services.AddControllers();` to the `Configure()` method.
- `endpoints.MapControllers();` to `app.UseEndpoints` into the `Configure()` method.

> Note: The above is sufficient to enable WebAPI. The default output is JSON. But it can be configured further for convenience and rigorous respect of naming between C# and JSON objects.

Replace `services.AddControllers();` with

```csharp
services.AddControllers()
            .AddJsonOptions(option => option.JsonSerializerOptions.WriteIndented = true)
            .AddJsonOptions(option => option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
```

> Note : `JsonNamingPolicy` is in `System.Text.Json`.

Create a `Controllers` folder in which the controllers will be made available.

Create a first action. Unlike ASP.NET MVC WebAPI, the return type is not `IHttpActionResult` but `IActionResult`
```
[HttpGet]
public IActionResult GetAll() { // ... Do your thing here ... }
```