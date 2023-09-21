using System.Text;
using RestService.Data;
using RestService.DatabaseDriver;
using RestService.DataMapper;
using RestService.Endpoint.Arguments;
using RestService.Extensions;
using RestService.Queries;

namespace RestService.Endpoint;

public class UserEndpoint : IAsyncEndpoint<NoArguments, User[]>, IAsyncEndpoint<User, bool>
{
    private readonly GetAllUsersQuery _usersQuery;
    private readonly AuthenticationQuery _authQuery;

    public UserEndpoint(GetAllUsersQuery usersQuery, AuthenticationQuery authQuery)
    {
        _usersQuery = usersQuery;
        _authQuery = authQuery;
    }

    public Task<User[]> GetAsync() => _usersQuery.Execute();
    
    public Task<User[]> GetAsync(NoArguments arguments)
    {
        throw new NotImplementedException();
    }

    public Task<bool> GetAsync(User arguments)
    {
        _authQuery.Execute(arguments);
    }
}