using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Timezone
{
    class Reader : IReader, IDisposable
    {
        public List<Tuple<string, string>> Read()
        {
            List<Tuple<string, string>> lReturn = new List<Tuple<string, string>>();

            string resourceFile = Properties.Resources.Timezone;
            string[] fileParts = resourceFile.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string part in fileParts)
            {
                string[] sLineParts = part.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Only add to list if there are items in the Array
                if ( sLineParts.Length == 2)
                { 
                    Tuple<string, string> timeZone = new Tuple<string, string>(sLineParts.First(), sLineParts.Last());
                    lReturn.Add(timeZone);
                }
                else
                {
                    Console.WriteLine($"Invalid input line \"{part}\", number of parts is {sLineParts.Length}");
                }
            }
            return lReturn;
        }
        public void Dispose()
        {

        }
    }
}
