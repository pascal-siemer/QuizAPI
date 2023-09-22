using System.Text;
using QuizAPI.Arguments;
using QuizAPI.Data;
using QuizAPI.Queries;

namespace QuizAPI.Endpoint;

public class UserEndpoint : IAsyncEndpoint<NoArguments, User[]>
{
    private readonly GetAllUsersQuery _query;
    
    public UserEndpoint(GetAllUsersQuery query)
    {
        _query = query;
    }

    public Task<User[]> GetAsync(NoArguments arguments) => _query.Execute(arguments);
}