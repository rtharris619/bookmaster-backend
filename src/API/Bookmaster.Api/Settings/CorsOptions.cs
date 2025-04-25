namespace Bookmaster.Api.Settings;

public sealed class CorsOptions
{
    public const string PolicyName = "BookmasterCorsPolicy";
    public const string SectionName = "Cors";

    public required string[] AllowedOrigins { get; init; }
}
