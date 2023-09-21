using Microsoft.Data.Sqlite;
using RestService.Configurations;
using RestService.Data;
using RestService.DatabaseDriver;
using RestService.DataMapper;
using RestService.Endpoint.Arguments;

namespace RestService.Queries;

public class AuthenticationQuery : IQuery<AuthenticationArguments, bool>
{
    private IDatabaseDriver _driver;
    private IDatabaseConfiguration _configuration;
    private IDataMapper<bool> _mapper;
    private string _query;

    public AuthenticationQuery(
        IDatabaseDriver driver,
        IDatabaseConfiguration configuration,
        IDataMapper<bool> mapper)
    {
        _driver = driver;
        _configuration = configuration;
        _mapper = mapper;
        _query = $"""
            SELECT EXISTS (
                SELECT [ID], [Username]
                FROM [main].[Users], (
                    SELECT
                        'pascal' as _username,
                        'test' as _password
                ) 
                WHERE Username = _username
                  and Password = _password
            );
            """;
    }


    public async Task<bool> Execute(AuthenticationArguments arguments)
    {
        await foreach (var entry in _driver.Read(_query, arguments, _mapper))
        {
            if (entry is not true)
            {
                continue;
            }

            return true;
        }

        return false;
    }
}