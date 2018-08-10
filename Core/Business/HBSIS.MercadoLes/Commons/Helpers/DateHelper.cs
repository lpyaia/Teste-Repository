using System;

namespace HBSIS.MercadoLes.Commons.Helpers
{
    public static class DateHelper
    {
        public static DateTime Now
        {
            get { return DateTime.UtcNow; }
        }

        public static DateTime MinValue
        {
            get { return new DateTime(0L, DateTimeKind.Utc); }
        }

        public static int GetTotalMinutes(DateTime startTime, DateTime endTime)
        {
            return (int)Math.Round((endTime - startTime).TotalMinutes, MidpointRounding.AwayFromZero);
        }

        public static DateTimeOffset ToOffset(this DateTime value)
        {
            var southAmerica = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            var utcOffset = new DateTimeOffset(value, TimeSpan.Zero);
            return utcOffset.ToOffset(southAmerica.GetUtcOffset(utcOffset));
        }
    }
}