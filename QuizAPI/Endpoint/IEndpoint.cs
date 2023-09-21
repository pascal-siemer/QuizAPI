namespace RestService.Endpoint;

public interface IEndpoint<TArguments, TResult>
{
    public TResult Get(TArguments arguments);
}