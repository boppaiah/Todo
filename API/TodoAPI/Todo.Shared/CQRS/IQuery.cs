using MediatR;


namespace Todo.Shared.CQRS
{
    public interface IQuery<out TResponse>: IRequest<TResponse>
    {
    }

    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery,TResponse>
        where TResponse : notnull
        where TQuery: IQuery<TResponse>
    {
    }
}
