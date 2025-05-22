﻿using Bookmaster.Common.Domain;

namespace Bookmaster.Common.Features.Messaging;

public interface IQueryHandler<in TQuery, TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}
