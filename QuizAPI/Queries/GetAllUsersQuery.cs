using QuizAPI.Arguments;
using QuizAPI.Configurations;
using QuizAPI.Data;
using QuizAPI.DatabaseDriver;
using QuizAPI.DataMapper;

namespace QuizAPI.Queries;

public class GetAllUsersQuery : IQuery<NoArguments, User[]>
{
    private IDatabaseDriver _driver;
    private IDatabaseConfiguration _configuration;
    private IDataMapper<User> _mapper;
    private string _query;
    
    public GetAllUsersQuery(IDatabaseDriver driver, IDatabaseConfiguration configuration, IDataMapper<User> mapper)
    {
        Console.WriteLine();
        _driver = driver;
        _configuration = configuration;
        _mapper = mapper;
        _query = $"""
            SELECT [ID], [Username], [Password]
            FROM {configuration.Schema}.[Users]
            """;
    }
    
    public async Task<User[]> Execute(NoArguments arguments)
    {
        var users = new List<User>();

        await foreach (var user in _driver.Read(_query, null, _mapper))
        {
            users.Add(user);
        }

        return users.ToArray();
    }

}