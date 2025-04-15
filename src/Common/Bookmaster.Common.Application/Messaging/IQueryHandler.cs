using Bookmaster.Common.Domain;
using MediatR;

namespace Bookmaster.Common.Features.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
