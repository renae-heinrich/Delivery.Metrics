using System;
using System.Globalization;

namespace Delivery.Metrics.Helpers
{
    public static class StringToUnixTimeHelper
    {
        public static long GetUnixTime(this string startTime)
        {
            //Utc time
            var dt = DateTime.ParseExact(startTime, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
            return ((DateTimeOffset)dt).ToUnixTimeSeconds();
            
            //1616630400 = GMT: Thursday, 25 March 2021 00:00:00
            //Your time zone: Thursday, 25 March 2021 11:00:00 GMT+11:00 DST - what is the impact of this??
        }
        
    }
}