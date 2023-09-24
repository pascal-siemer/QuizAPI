using Npgsql;
using QuizAPI.DataMapper;

namespace QuizAPI.Database.Interfaces;

public interface IDatabaseDriver
{
    public IAsyncEnumerable<TResult> Read<TResult>(string query, IEnumerable<NpgsqlParameter>? parameters, IDataMapper<TResult> dataMapper);

    public Task Cancel();
}