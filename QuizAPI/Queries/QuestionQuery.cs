using Microsoft.Data.Sqlite;
using QuizAPI.Arguments;
using QuizAPI.Configurations;
using QuizAPI.Data;
using QuizAPI.DatabaseDriver;
using QuizAPI.DataMapper;

namespace QuizAPI.Queries;

public class QuestionQuery: IQuery<IdentifierArguments, Question?>
{
    private IDatabaseDriver _driver;
    private IDatabaseConfiguration _configuration;
    private IDataMapper<string> _choiceMapper;
    private IDataMapper<Question> _questionMapper;
    private string _choicesQuery;
    private string _questionsQuery;
    
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
        
        _choicesQuery = $"""
            select
                Choices.Title as Choice
            from Questions_Choices
                inner join Questions on Questions.ID = Questions_Choices.QuestionID
                inner join Choices on Choices.ID = Questions_Choices.ChoiceID
            where Questions.ID = $ID;
            """;

        _questionsQuery = $"""
            select 
                Questions.ID as ID,
                Prompts.Title as Prompt,
                Choices.Title as Answer
            from Questions
                inner join Prompts on Prompts.ID = Questions.PromptID
                inner join Choices on Choices.ID = Questions.CorrectChoiceID
            where Questions.ID = $ID;
              
            """;
    }
    
    public async Task<Question?> Execute(IdentifierArguments arguments)
    {
        var parameters = CreateParameters(arguments);

        var question = await ExecuteQuestionQuery(parameters);

        if (question is null)
        {
            return null;
        }

        var choices = await ExecuteChoicesQuery(parameters);

        return question with { Choices = choices };
    }

    private async Task<List<string>> ExecuteChoicesQuery(IEnumerable<SqliteParameter> parameters)
    {
        var choices = new List<string>();
        
        await foreach (var entry in _driver.Read(_choicesQuery, parameters, _choiceMapper))
        {
            choices.Add(entry);
        }

        return choices;
    }

    private async Task<Question?> ExecuteQuestionQuery(IEnumerable<SqliteParameter> parameters)
    {
        await foreach (var entry in _driver.Read(_questionsQuery, parameters, _questionMapper))
        {
            return entry;
        }

        return null;
    }

    private IEnumerable<SqliteParameter> CreateParameters(IdentifierArguments arguments) => new List<SqliteParameter>
    {
        new ("ID", arguments.Identifier)
    };
}