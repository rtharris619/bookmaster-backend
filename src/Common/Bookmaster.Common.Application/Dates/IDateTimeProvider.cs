namespace Bookmaster.Common.Features.Dates;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }

    DateTime? ConvertToUtc(DateTime? date);
}
