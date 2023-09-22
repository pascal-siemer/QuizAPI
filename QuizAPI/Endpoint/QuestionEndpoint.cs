using QuizAPI.Arguments;
using QuizAPI.Data;
using QuizAPI.Queries;

namespace QuizAPI.Endpoint;

public class QuestionEndpoint: IAsyncEndpoint<IdentifierArguments, Question?>
{
    private readonly QuestionQuery _query;

    public QuestionEndpoint(QuestionQuery query)
    {
        _query = query;
    }

    public Task<Question?> GetAsync(IdentifierArguments arguments) => _query.Execute(arguments);
}