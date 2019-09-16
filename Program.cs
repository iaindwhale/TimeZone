using System;
using System.Collections.Generic;
using System.Configuration;

namespace Timezone
{
    class Program
    {        
        static void Main(string[] args)
        {
            //Create instance of TimeZoneManager and inject into timeZoneParser to lookup timezones.  
            TimeZoneManager tzm = new TimeZoneManager();
            IParser timeZoneParser = new Parser(tzm);

            using (Reader fileReader = new Reader())
            {
                List<Tuple<string, string>> lTimes = fileReader.Read();

                Console.WriteLine();
                Console.WriteLine("Parsing Results");
                Console.WriteLine("---------------");

                foreach ( Tuple<string, string> time in lTimes)
                {
                    timeZoneParser.DisplayTime(time.Item1, time.Item2);
                }
                Console.WriteLine("Parsing Complete, hit <RETURN> to continue.");
                Console.ReadLine();
            }
        }
    }
}
