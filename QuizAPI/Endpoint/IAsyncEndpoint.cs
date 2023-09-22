namespace QuizAPI.Endpoint;

public interface 
IAsyncEndpoint<TArguments, TResult>
{
    public Task<TResult> GetAsync(TArguments arguments);
}