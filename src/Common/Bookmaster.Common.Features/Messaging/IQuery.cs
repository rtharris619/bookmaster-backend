using Bookmaster.Common.Domain;
using MediatR;

namespace Bookmaster.Common.Features.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
