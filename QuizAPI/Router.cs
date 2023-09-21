using RestService.Endpoint;

namespace RestService;

public static class Router
{
    public static void InitRoutes(WebApplication app)
    {
        SetDefaultRoute(app);
        SetUserRoutes(app);
    }

    private static void SetDefaultRoute(WebApplication app)
    {
        app.MapGet("/", () => "Hello World!");
    }
    
    private static void SetUserRoutes(WebApplication app)
    {
        app.MapGet("/users", (UserEndpoint endpoint) => endpoint.GetAsync());
    }

    private static void SetAuthenticationRoute(WebApplication app)
    {
        app.MapGet("/auth/{username}/{password}", (UserEndpoint endpoint, string username, string password) => endpoint.AuthenticateAsync(username, password));
    }
}