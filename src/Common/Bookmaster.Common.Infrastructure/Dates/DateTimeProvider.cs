using Bookmaster.Common.Features.Dates;

namespace Bookmaster.Common.Infrastructure.Dates;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime? ConvertToUtc(DateTime? date)
    {
        return date?.Kind == DateTimeKind.Utc ? date : date?.ToUniversalTime();
    }
}
