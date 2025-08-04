namespace Bookmaster.Common.Features.Authorization;

public sealed record PermissionsResponse(Guid UserId, HashSet<string> Permissions);
