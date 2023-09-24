using Microsoft.Data.Sqlite;
using Npgsql;
using QuizAPI.Configurations;
using QuizAPI.Database.Interfaces;
using QuizAPI.DataMapper;

namespace QuizAPI.Database.Postgres;

public class PostgresDriver : IDatabaseDriver
{
    
    /*
     
     Fragen: 
     Dieser Driver hat eine Connection, die IDisposable Implementiert. Sollte dann IDatabaseDriver auch IDisposable Implementieren oder nur der PostgresDriver direkt?
     
     Vorgehensweisen beim Lesen:
     
     Ich will nicht immer alle Daten verarbeiten, wenn ich nicht alle Daten brauche.
     - nicht yielden, sondern Liste bauen und diese dann verarbeiten.
     - Cancel()-Methode implementieren, die bei Early Returns aufgerufen werden muss.
     - Daten aus dem Reader in eine Liste zusammenfassen, aber weitere verarbeitung yielden
     
     */

    private NpgsqlConnection _connection;

    public PostgresDriver(IDatabaseConfiguration configuration)
    {
        _connection = new(configuration.ConnectionString);
    }
    
    
    
    public async IAsyncEnumerable<TResult> Read<TResult>(string query, IEnumerable<NpgsqlParameter>? parameters, IDataMapper<TResult> dataMapper)
    {
        await _connection.OpenAsync();

        var reader = await ExecuteReaderAsync(query, parameters);

        while (await reader.ReadAsync())
        {
            yield return dataMapper.Map(reader);
        }

        await _connection.CloseAsync();
        
    }

    public Task Cancel() => _connection.CloseAsync();

    private Task<NpgsqlDataReader> ExecuteReaderAsync(string query, IEnumerable<NpgsqlParameter>? parameters)
    {
        var command = new NpgsqlCommand(query, _connection);

        if (parameters is not null)
        {
            command.Parameters.AddRange(parameters.ToArray());
        }

        return command.ExecuteReaderAsync();
    }
    

}






