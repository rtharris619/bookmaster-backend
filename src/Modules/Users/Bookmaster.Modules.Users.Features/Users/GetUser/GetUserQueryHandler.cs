using System.Data.Common;
using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Data;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Users.Domain.Users;
using Dapper;

namespace Bookmaster.Modules.Users.Features.Users.GetUser;

internal sealed class GetUserQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(UserResponse.Id)},
                 email AS {nameof(UserResponse.Email)},
                 first_name AS {nameof(UserResponse.FirstName)},
                 last_name AS {nameof(UserResponse.LastName)},
                 identity_id AS {nameof(UserResponse.IdentityId)}
             FROM users.users
             WHERE id = @UserId
             """;

        UserResponse? user = await connection.QuerySingleOrDefaultAsync<UserResponse>(sql, query);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(query.UserId));
        }

        return user;
    }
}
