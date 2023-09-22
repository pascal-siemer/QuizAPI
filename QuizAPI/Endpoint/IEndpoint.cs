namespace QuizAPI.Endpoint;

public interface IEndpoint<TArguments, TResult>
{
    public TResult Get(TArguments arguments);
}