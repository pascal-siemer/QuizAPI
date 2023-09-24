using Microsoft.Data.Sqlite;
using QuizAPI.Configurations;
using QuizAPI.Database.Interfaces;
using QuizAPI.DataMapper;

namespace QuizAPI.Database.Sqlite;

public class SqliteDriver: IDisposable, IAsyncDisposable //IDatabaseDriver
{
    private SqliteConnection _connection;

    public SqliteDriver(IDatabaseConfiguration configuration)
    {
        _connection = new(configuration.ConnectionString);
    }
    
    public async IAsyncEnumerable<TResult> Read<TResult>(string query, IEnumerable<SqliteParameter>? parameters, IDataMapper<TResult> dataMapper)
    {
        await _connection.OpenAsync();

        var reader = await ExecuteReaderAsync(query, parameters);

        while (await reader.ReadAsync())
        {
            yield return dataMapper.Map(reader);
        }

        await _connection.CloseAsync();
    }

    private Task<SqliteDataReader> ExecuteReaderAsync(string query, IEnumerable<SqliteParameter>? parameters)
    {
        var command = new SqliteCommand(query, _connection);
        
        if (parameters is not null) {
            command.Parameters.AddRange(parameters);
        }
        return command.ExecuteReaderAsync();
    }

    public void Dispose() => _connection.Dispose();

    public ValueTask DisposeAsync() =>_connection.DisposeAsync();

}