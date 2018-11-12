using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commonality.instrument
{
    /// <summary>
    /// UTC时间转换
    /// </summary>
   public class UTC
    {
        public static long ConvertDateTimeLong(DateTime Time)//DateTime time = System.DateTime.UtcNow;
        {
            double doubleResult = 0;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime endTime = Time;
            doubleResult = (endTime - startTime).TotalSeconds;
            return (long)(doubleResult);
        }

        public static DateTime ConvertLongDateTime(long UTCTime)
        {
            DateTime time = DateTime.MinValue;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            time = startTime.AddSeconds(UTCTime);
            return time;
        }
    }
}
