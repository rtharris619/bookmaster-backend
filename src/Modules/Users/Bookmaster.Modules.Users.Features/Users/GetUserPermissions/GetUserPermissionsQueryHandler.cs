﻿using System.Data.Common;
using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Authorization;
using Bookmaster.Common.Features.Data;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Users.Domain.Users;
using Dapper;

namespace Bookmaster.Modules.Users.Features.Users.GetUserPermissions;

internal sealed class GetUserPermissionsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetUserPermissionsQuery, PermissionsResponse>
{
    public async Task<Result<PermissionsResponse>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT DISTINCT
                 u.id AS {nameof(UserPermission.UserId)},
                 rp.permission_code AS {nameof(UserPermission.Permission)}
             FROM users.users u
             JOIN users.user_roles ur ON ur.user_id = u.id
             JOIN users.role_permissions rp ON rp.role_name = ur.role_name
             WHERE u.identity_id = @IdentityId
             """;

        List<UserPermission> permissions = (await connection.QueryAsync<UserPermission>(sql, request)).AsList();

        if (!permissions.Any())
        {
            return Result.Failure<PermissionsResponse>(UserErrors.NotFound(request.IdentityId));
        }

        return new PermissionsResponse(permissions[0].UserId, permissions.Select(p => p.Permission).ToHashSet());
    }

    internal sealed class UserPermission
    {
        internal Guid UserId { get; init; }

        internal string Permission { get; init; }
    }
}
