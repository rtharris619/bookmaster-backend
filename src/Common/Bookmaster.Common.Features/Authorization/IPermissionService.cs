using Bookmaster.Common.Domain;

namespace Bookmaster.Common.Features.Authorization;

public interface IPermissionService
{
    Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId);
}
