using System.Collections.Immutable;
using System.ComponentModel;
using RestService.Configurations;
using RestService.Data;
using RestService.DatabaseDriver;
using RestService.DataMapper;
using RestService.Endpoint;
using RestService.Queries;

namespace RestService;

public static class Services
{
    public static void InitServices(WebApplicationBuilder builder)
    {
        InitDatabase(builder, DatabaseType.Sqlite);
        InitDataMapper(builder);
        InitQueries(builder);
        InitEndpoints(builder);
    }

    private static void InitDatabase(WebApplicationBuilder builder, DatabaseType database)
    {
        switch (database)
        {
            case DatabaseType.MSSQL: InitMSSQL(builder); break;
            case DatabaseType.Sqlite: InitSqlite(builder); break;
            default: throw new InvalidEnumArgumentException();
        }
    }

    private static void InitMSSQL(WebApplicationBuilder builder)
    {
        InitDatabaseConfiguration(builder, "./Configurations/MSSQLConfiguration.json");
        builder.Services.AddSingleton<IDatabaseDriver, MSSQLDriver>();
    }

    private static void InitSqlite(WebApplicationBuilder builder)
    {
        InitDatabaseConfiguration(builder, "./Configurations/SqliteConfiguration.json");
        builder.Services.AddSingleton<IDatabaseDriver, SqliteDriver>();
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
    }
    
    private static void InitQueries(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<GetAllUsersQuery>();
        builder.Services.AddSingleton<AuthenticationQuery>();
    }

    private static void InitEndpoints(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<UserEndpoint>();
    }
}