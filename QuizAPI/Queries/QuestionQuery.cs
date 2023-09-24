using Microsoft.Data.Sqlite;
using Npgsql;
using QuizAPI.Arguments;
using QuizAPI.Configurations;
using QuizAPI.Data;
using QuizAPI.Database.Interfaces;
using QuizAPI.DataMapper;

namespace QuizAPI.Queries;

public class QuestionQuery: IQuery<IdentifierArguments, Question?>
{
    private IDatabaseDriver _driver;
    private IDatabaseConfiguration _configuration;
    private IDataMapper<string> _choiceMapper;
    private IDataMapper<Question> _questionMapper;
    private string _query;
    
    public QuestionQuery(
        IDatabaseDriver driver,
        IDatabaseConfiguration configuration,
        IDataMapper<string> choiceMapper,
        IDataMapper<Question> questionMapper)
    {
        _driver = driver;
        _configuration = configuration;
        _choiceMapper = choiceMapper;
        _questionMapper = questionMapper;
        _query = "select row_to_json(get_question(@id))";
    }

    public async Task<Question?> Execute(IdentifierArguments arguments)
    {
        var parameters = CreateParameters(arguments);
        
        await foreach (var entry in _driver.Read(_query, parameters, _questionMapper))
        {
            await _driver.Cancel();
            
            return entry;
        }

        return null;
    }
    
    // public async Task<Question?> ExecuteOld(IdentifierArguments arguments)
    // {
    //     var parameters = CreateParameters(arguments);
    //
    //     var question = await ExecuteQuestionQuery(parameters);
    //
    //     if (question is null)
    //     {
    //         return null;
    //     }
    //
    //     var choices = await ExecuteChoicesQuery(parameters);
    //
    //     return question with { Choices = choices };
    // }
    //
    // private async Task<List<string>> ExecuteChoicesQuery(IEnumerable<NpgsqlParameter> parameters)
    // {
    //     var choices = new List<string>();
    //     
    //     await foreach (var entry in _driver.Read(_choicesQuery, parameters, _choiceMapper))
    //     {
    //         choices.Add(entry);
    //     }
    //
    //     return choices;
    // }
    //
    // private async Task<Question?> ExecuteQuestionQuery(IEnumerable<NpgsqlParameter> parameters)
    // {
    //     await foreach (var entry in _driver.Read(_questionsQuery, parameters, _questionMapper))
    //     {
    //         await _driver.Cancel();
    //         
    //         return entry;
    //     }
    //
    //     return null;
    // }

    private IEnumerable<NpgsqlParameter> CreateParameters(IdentifierArguments arguments)
    {
        yield return new("id", arguments.Identifier);
    }
}