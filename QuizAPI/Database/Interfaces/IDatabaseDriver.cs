using System.Data.SqlClient;
using RestService.DataMapper;

namespace RestService.DatabaseDriver;

public interface IDatabaseDriver
{
    public IAsyncEnumerable<TResult> Read<TResult>(string query, SqlParameter parameter, IDataMapper<TResult> dataMapper);
}