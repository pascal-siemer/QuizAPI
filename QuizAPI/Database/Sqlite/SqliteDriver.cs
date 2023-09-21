using Microsoft.Data.Sqlite;
using RestService.Configurations;
using RestService.DataMapper;

namespace RestService.DatabaseDriver;

public class SqliteDriver: IDatabaseDriver, IDisposable, IAsyncDisposable
{
    private SqliteConnection _connection;

    public SqliteDriver(IDatabaseConfiguration configuration)
    {
        _connection = new(configuration.ConnectionString);
    }
    
    public async IAsyncEnumerable<TResult> Read<TArguments, TResult>(string query, TArguments arguments, IDataMapper<TResult> dataMapper)
    {
        await _connection.OpenAsync();

        var reader = await CreateReaderAsync(query);

        while (await reader.ReadAsync())
        {
            yield return dataMapper.Map(reader);
        }

        await _connection.CloseAsync();
    }

    private Task<SqliteDataReader> CreateReaderAsync(string query, IEnumerable<SqliteParameter> parameters)
    {
        var command = _connection.CreateCommand();
        command.CommandText = query;
        command.Parameters.AddRange(parameters);
        return command.ExecuteReaderAsync();
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _connection.DisposeAsync();
    }
}