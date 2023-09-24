using Microsoft.Data.Sqlite;
using Npgsql;
using QuizAPI.Arguments;
using QuizAPI.Configurations;
using QuizAPI.Database.Interfaces;
using QuizAPI.DataMapper;

namespace QuizAPI.Queries;

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
            select exists (
                select id, username
            from {configuration.Schema}.users
            where username = @username
              and password_hash = @password_hash
            );
            """;
    }


    public async Task<bool> Execute(AuthenticationArguments arguments)
    {
        var parameters = CreateParameters(arguments);
        
        await foreach (var entry in _driver.Read(_query, parameters, _mapper))
        {
            if (entry is not true)
            {
                continue;
            }

            await _driver.Cancel();

            return true;
        }

        return false;
    }

    private IEnumerable<NpgsqlParameter> CreateParameters(AuthenticationArguments arguments) =>
        new List<NpgsqlParameter>
        {
            new("username", arguments.Username),
            new("password_hash", arguments.Password)
        };

}