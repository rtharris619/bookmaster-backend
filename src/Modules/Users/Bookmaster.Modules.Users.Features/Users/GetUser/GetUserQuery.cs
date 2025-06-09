using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Users.Features.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
