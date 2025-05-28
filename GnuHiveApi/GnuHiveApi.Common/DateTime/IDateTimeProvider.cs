namespace GnuHiveApi.Common.DateTime;

public interface IDateTimeProvider
{
    System.DateTime UtcNow { get; }

    System.DateTime SastNow { get; }
    
    DateTimeOffset OffsetUtcNow { get; }
    
    System.DateTime FromOffsetToSast(DateTimeOffset dateTimeOffset);
}

public class SystemDateTimeProvider : IDateTimeProvider
{
    private static readonly TimeZoneInfo _sast = TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time");

    public System.DateTime UtcNow => System.DateTime.UtcNow;
    public DateTimeOffset OffsetUtcNow => DateTimeOffset.UtcNow;

    public System.DateTime SastNow => this.FromOffsetToSast(this.OffsetUtcNow);

    public System.DateTime FromOffsetToSast(DateTimeOffset dateTimeOffset)
    {
        return System.DateTime.SpecifyKind(TimeZoneInfo.ConvertTime(dateTimeOffset.UtcDateTime, _sast),
            DateTimeKind.Local);
    }
}