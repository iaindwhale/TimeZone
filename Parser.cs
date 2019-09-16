using System;

namespace Timezone
{
    class Parser : IParser
    {
        private TimeZoneManager tzm;
        public Parser(TimeZoneManager tzMgr)
        {
            this.tzm = tzMgr;
        }

        /// <summary>
        /// Calculates the time in the new timezone and displays UTC time, location and calcualted time in TimeZone that location belongs to.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timezone"></param>
        public void DisplayTime(string time, string timezone)
        {
            try
            {
                DateTime utcTime = DateTime.Parse(time);
                TimeZoneInfo tzi = this.tzm.FindTimeZone(timezone);

                if (tzi != null)
                {
                    // Convert time to new TimeZone
                    TimeSpan offsetTime = tzi.GetUtcOffset(utcTime);
                    DateTime convertedTime = utcTime.Add(offsetTime);
                    Console.WriteLine($"The time in the UK is {utcTime.ToLongTimeString()} and the time in {timezone} is {convertedTime.ToLongTimeString()}");
                }
                else
                {
                    Console.WriteLine($"{timezone} cannot be mapped to a Timezone.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine($"{time} is an invalid time, therefore the time in {timezone} cannot be calculated."); 
            }
        }
    }
}
