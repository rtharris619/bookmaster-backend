using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Exceptions;
using Bookmaster.Common.Features.Messaging;
using Microsoft.Extensions.Logging;

namespace Bookmaster.Common.Features.Behaviors;

internal static class ExceptionHandlingDecorator
{
    internal sealed class CommandBaseHandler<TCommand>(
        ICommandHandler<TCommand> inner,
        ILogger<CommandBaseHandler<TCommand>> logger)
        : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await inner.Handle(command, cancellationToken);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Unhandled exception for {RequestName}", typeof(TCommand).Name);
                throw new BookmasterException(typeof(TCommand).Name, innerException: exception);
            }
        }
    }

    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> inner,
        ILogger<CommandHandler<TCommand, TResponse>> logger)
        : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await inner.Handle(command, cancellationToken);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Unhandled exception for {RequestName}", typeof(TCommand).Name);
                throw new BookmasterException(typeof(TCommand).Name, innerException: exception);
            }
        }
    }

    internal sealed class QueryHandler<TQuery, TResponse>(
        IQueryHandler<TQuery, TResponse> inner,
        ILogger<QueryHandler<TQuery, TResponse>> logger)
        : IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
        {
            try
            {
                return await inner.Handle(query, cancellationToken);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Unhandled exception for {RequestName}", typeof(TQuery).Name);
                throw new BookmasterException(typeof(TQuery).Name, innerException: exception);
            }
        }
    }
}
