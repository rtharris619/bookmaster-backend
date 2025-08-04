using Bookmaster.Common.Features.Authorization;
using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Users.Features.Users.GetUserPermissions;

public sealed record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionsResponse>;
