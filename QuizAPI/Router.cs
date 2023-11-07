using QuizAPI.Arguments;
using QuizAPI.Endpoint;

namespace QuizAPI;

public static class Router
{
    public static WebApplication MapRoutes(this WebApplication app)
    {
        SetDefaultRoute(app);
        SetUserRoute(app);
        SetAuthenticationRoute(app);
        SetQuestionRoute(app);

        return app;
    }

    private static void SetDefaultRoute(IEndpointRouteBuilder app) => app.MapGet(
        "/",
        () => "Hello World!");
    
    private static void SetUserRoute(IEndpointRouteBuilder app) => app.MapGet(
        "/users", 
        (UserEndpoint endpoint) => endpoint.GetAsync(new()));
    
    private static void SetAuthenticationRoute(IEndpointRouteBuilder app) => app.MapGet(
        "/auth/{username}/{password}", 
        (AuthenticationEndpoint endpoint, string username, string password) => endpoint.GetAsync(new(username, password)));

    private static void SetQuestionRoute(IEndpointRouteBuilder app) => app.MapGet(
        "/question/{identifier:int}", 
        (QuestionEndpoint endpoint, int identifier) => endpoint.GetAsync(new (identifier)));
}