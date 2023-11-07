using QuizAPI;

await WebApplication.CreateBuilder(args)
    .RegisterServices()
    .Build()
    .MapRoutes()
    .RunAsync();
    