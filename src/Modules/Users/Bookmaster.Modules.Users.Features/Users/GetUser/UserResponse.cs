namespace Bookmaster.Modules.Users.Features.Users.GetUser;

public sealed record UserResponse(Guid Id, string Email, string FirstName, string LastName, string IdentityId);
