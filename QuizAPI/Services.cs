using System.Collections.Immutable;
using System.ComponentModel;
using QuizAPI.Configurations;
using QuizAPI.DataMapper;
using QuizAPI.Data;
using QuizAPI.Database.Interfaces;
using QuizAPI.Database.Postgres;
using QuizAPI.Database.Sqlite;
using QuizAPI.Endpoint;
using QuizAPI.Queries;

namespace QuizAPI;

public static class Services
{
    public static void InitServices(WebApplicationBuilder builder)
    {
        InitDatabase(builder);
        InitDataMapper(builder);
        InitQueries(builder);
        InitEndpoints(builder);
    }

    private static void InitDatabase(WebApplicationBuilder builder)
    {
        InitDatabaseConfiguration(builder, "./Database/Postgres/PostgresConfiguration.json");
        builder.Services.AddTransient<IDatabaseDriver, PostgresDriver>();
    }

    private static void InitDatabaseConfiguration(WebApplicationBuilder builder, string path)
    {
        var configuration = new DatabaseConfiguration();
        builder.Configuration.AddJsonFile(path).Build();
        builder.Configuration.Bind(configuration);
        builder.Services.AddSingleton<IDatabaseConfiguration>(configuration);
    }

    private static void InitDataMapper(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IDataMapper<User>, UserMapper>();
        builder.Services.AddSingleton<IDataMapper<bool>, BooleanMapper>();
        builder.Services.AddSingleton<IDataMapper<string>, StringMapper>();
        builder.Services.AddSingleton<IDataMapper<Question>, QuestionMapper>();
    }
    
    private static void InitQueries(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<GetAllUsersQuery>();
        builder.Services.AddSingleton<AuthenticationQuery>();
        builder.Services.AddSingleton<QuestionQuery>();
    }

    private static void InitEndpoints(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<UserEndpoint>();
        builder.Services.AddSingleton<AuthenticationEndpoint>();
        builder.Services.AddSingleton<QuestionEndpoint>();
    }
}