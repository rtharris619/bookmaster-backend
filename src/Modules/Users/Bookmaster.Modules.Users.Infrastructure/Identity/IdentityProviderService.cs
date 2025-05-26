﻿using Bookmaster.Common.Domain;
using Bookmaster.Modules.Users.Features.Abstractions.Identity;
using Microsoft.Extensions.Logging;

namespace Bookmaster.Modules.Users.Infrastructure.Identity;

internal sealed class IdentityProviderService(
    KeyCloakClient keyCloakClient,
    ILogger<IdentityProviderService> logger) : IIdentityProviderService
{
    private const string PasswordCredentialType = "Password";

    public async Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        var userRepresentation = new UserRepresentation(
            user.Email,
            user.Email,
            user.FirstName,
            user.LastName,
            true,
            true,
            [new CredentialRepresentation(PasswordCredentialType, user.Password, false)]);

        try
        {
            string identityId = await keyCloakClient.RegisterUserAsync(userRepresentation, cancellationToken);
            return identityId;
        }
        catch (HttpRequestException exception) when (exception.StatusCode == System.Net.HttpStatusCode.Conflict)
        {
            logger.LogError(exception, "Failed to register user with email {Email}", user.Email);
            return Result.Failure<string>(IdentityProviderErrors.EmailIsNotUnique);
        }
    }
}
