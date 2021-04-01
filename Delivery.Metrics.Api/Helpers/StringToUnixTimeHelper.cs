using System;
using NodaTime;
using NodaTime.Text;

namespace Delivery.Metrics.Helpers
{
    public static class StringToUnixTimeHelper
    {
        
        public static long GetUnixTime(this string localDate)
        { 
            var utcTime = LocalTimeToUtc(localDate);
            return utcTime.ToUnixTimeSeconds();
        }
        
        static DateTimeOffset LocalTimeToUtc(string localDate)
        {
            var pattern = LocalDateTimePattern.CreateWithInvariantCulture("dd/MM/yyyy");
            LocalDateTime ldt = pattern.Parse(localDate).Value;
            ZonedDateTime zdt = ldt.InZoneLeniently(DateTimeZoneProviders.Tzdb["Australia/Sydney"]);
            Instant instant = zdt.ToInstant();
            ZonedDateTime utc = instant.InUtc();

            return utc.ToDateTimeOffset();
        }
    }
}