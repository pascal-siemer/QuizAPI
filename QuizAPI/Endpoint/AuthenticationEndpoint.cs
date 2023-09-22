using QuizAPI.Arguments;
using QuizAPI.Queries;

namespace QuizAPI.Endpoint;

public class AuthenticationEndpoint: IAsyncEndpoint<AuthenticationArguments, bool>
{
    private readonly AuthenticationQuery _query;
    
    public AuthenticationEndpoint(AuthenticationQuery query)
    {
        _query = query;
    }

    public Task<bool> GetAsync(AuthenticationArguments arguments) => _query.Execute(arguments);
    
}