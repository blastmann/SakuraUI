using System;

namespace SakuraUI.Utilities
{
    public static class TimeSpanExtensions
    {
        public static string ToReadableString(this TimeSpan time, string lessOneHourFormat = @"mm\:ss")
        {
            var format = time.TotalHours > 1 ? @"h\:mm\:ss" : lessOneHourFormat;
            return time.ToString(format);
        }
    }
}
