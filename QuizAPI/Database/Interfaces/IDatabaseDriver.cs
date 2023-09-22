using Microsoft.Data.Sqlite;
using QuizAPI.DataMapper;

namespace QuizAPI.DatabaseDriver;

public interface IDatabaseDriver
{
    public IAsyncEnumerable<TResult> Read<TResult>(string query, IEnumerable<SqliteParameter>? parameters, IDataMapper<TResult> dataMapper);
}