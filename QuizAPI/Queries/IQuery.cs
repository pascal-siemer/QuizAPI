namespace RestService.Queries;

public interface IQuery<TResult>
{
    public Task<TResult> Execute();
}

public interface IQuery<TArguments, TResult>
{
    public Task<TResult> Execute(TArguments arguments);
}
