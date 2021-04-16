Book List MVC
=============

## Views with ASP.NET Core MVC

The first part of the course did use Razor pages which roughly share a similar philosophy to XAML and its code-behind. This was the new part for me. I used Razor syntax in MVC views before.
The `Pages` folder is kind of divided on several folders:
- `Models`: Just the same as in the BookListRazor version of the application. Just that because it is expected in ASP.NET (Core) MVC, we did not need to add it manually.
- `Views`: Will contain subfolders named after the Controller they relate to. In side these are the `cshtml` files for these controllers.
- `Controllers`: Will contain files named after the pattern `<Singular controller name>Controller.cs`. May also contain Web API Controllers. In such case the practice is to separate those in the `Controllers\api` folder.

> Note: Remember the difference between MVC and Web API controllers is they respectively derive `Controller` and `ControllerBase`. This means the available API is different in each of them.

In MVC controllers, actions are given the name of a related view. For instance the `Index` view for a controller is rendered by calling its `Index()` action. ASP.NET will return the view from the `Views` folder.

For instance:
>Controllers\MyController.cs
```csharp
public class MyController: Controller{
	// ...
	public IActionResult Index(){ /* ... */ }
	// ...
}
```

Will return the `cshtml` from `Views\My\Index.cshtml`.

## Routing

MVC project comes with URL template in `Startup.cs`

```csharp
app.UseRouting();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
});
```

In comparison of the .NET Framework version ASP.NET MVC in which routing is condfigured in `App_Start\RouteConfig.cs`

> A config of routing in ASP.NET MVC with .NET Framework. For reference.
```
public static void RegisterRoutes(RouteCollection routes)
{
	routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

	routes.MapMvcAttributeRoutes();

	routes.MapRoute(
			name: "Default",
			url: "{controller}/{action}/{id}",
			defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
	);
}
```
## DB Setup

As usual we need to add the connection string in `appsettings.json`
```javascript
"ConnectionStrings": {
    "DefaultConnection": "Server=(LocalDB)\\MSSQLLocalDB;Database=BookListRazor;Trusted_Connection=True;MultipleActiveResultSets=True"
  },
```

And instruct to make use of it in `Startup.ConfigureServices()`.
```

```