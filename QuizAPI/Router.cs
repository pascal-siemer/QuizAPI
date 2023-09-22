using QuizAPI.Arguments;
using QuizAPI.Endpoint;

namespace QuizAPI;

public static class Router
{
    public static void InitRoutes(WebApplication app)
    {
        SetDefaultRoute(app);
        SetUserRoute(app);
        SetAuthenticationRoute(app);
        SetQuestionRoute(app);
    }

    private static void SetDefaultRoute(WebApplication app) => app.MapGet(
        "/",
        () => "Hello World!");
    
    private static void SetUserRoute(WebApplication app) => app.MapGet(
        "/users", 
        (UserEndpoint endpoint) => endpoint.GetAsync(new()));
    
    private static void SetAuthenticationRoute(WebApplication app) => app.MapGet(
        "/auth/{username}/{password}", 
        (AuthenticationEndpoint endpoint, string username, string password) => endpoint.GetAsync(new(username, password)));

    private static void SetQuestionRoute(WebApplication app) => app.MapGet(
        "/question/{identifier:int}", 
        (QuestionEndpoint endpoint, int identifier) => endpoint.GetAsync(new (identifier)));
}