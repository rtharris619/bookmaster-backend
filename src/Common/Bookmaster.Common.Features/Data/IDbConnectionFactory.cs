using System.Data.Common;

namespace Bookmaster.Common.Features.Data;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
