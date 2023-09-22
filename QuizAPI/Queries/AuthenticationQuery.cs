using Microsoft.Data.Sqlite;
using QuizAPI.Arguments;
using QuizAPI.Configurations;
using QuizAPI.DatabaseDriver;
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
            SELECT EXISTS (
                SELECT [ID], [Username]
                FROM {configuration.Schema}.[Users], (
                    SELECT
                        'pascal' as _username,
                        'test' as _password
                ) 
                WHERE Username = $Username
                  and Password = $Password
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

            return true;
        }

        return false;
    }
    
    private IEnumerable<SqliteParameter> CreateParameters(AuthenticationArguments arguments) => new List<SqliteParameter>
    {
        new ("Username", arguments.Username),
        new ("Password", arguments.Password)
    };
    
}