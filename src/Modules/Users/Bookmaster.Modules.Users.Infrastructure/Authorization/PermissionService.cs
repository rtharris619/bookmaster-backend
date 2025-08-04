using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Authorization;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Users.Features.Users.GetUserPermissions;

namespace Bookmaster.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(
    IQueryHandler<GetUserPermissionsQuery, PermissionsResponse> handler,
    CancellationToken cancellationToken = default) 
    : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
    {
        return await handler.Handle(new GetUserPermissionsQuery(identityId), cancellationToken);
    }
}
