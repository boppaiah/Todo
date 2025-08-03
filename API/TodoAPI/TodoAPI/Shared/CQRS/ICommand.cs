using MediatR;


namespace TodoAPI.Shared.CQRS
{
    public interface ICommand<out TResponse>:IRequest<TResponse>
    {
    }

    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest,TResponse>
        where TRequest: ICommand<TResponse>
        where TResponse: notnull
    {
    }
}
