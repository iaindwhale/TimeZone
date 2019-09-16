using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Timezone
{
    /// <summary>
    /// TimeZoneManager retains a HashTable of Geographic names against their TimeZoneID, as defined in the TimeZoneInfo class.  It can then be looked up
    /// to find the TimeZoneID for a specific Geographic name.
    /// </summary>
    public class TimeZoneManager : ITimeZoneManager
    {
        // A Look-up table of Names against TimeZone IDs.  The names could be any geographic location within the time-zone, however, each name MUST be unique
        private System.Collections.Hashtable timeZoneIndex;

        // A list of Time Zone names that will be excluded from the TimeZoneIndex
        private List<string> exceptionNames;

        public TimeZoneManager()
        {
            this.CreateExceptionsList();
            this.CreateTimeZoneIndex();
        }

        /// <summary>
        /// The excepton list is a list of TimeZone names that are excluded.  The  reasons that they are excluded are that either they are invalid/old place names
        /// or are not geographic names.  Ideally this should be in external storage (database or text file) so that it is easier to update.
        /// </summary>
        private void CreateExceptionsList()
        {
            // Create the exception list.  
            this.exceptionNames = new List<string>();

            this.exceptionNames.Add("Petropavlovsk-Kamchatsky - Old");
            this.exceptionNames.Add("Coordinated Universal Time");
            this.exceptionNames.Add("Coordinated Universal Time-01");
            this.exceptionNames.Add("Coordinated Universal Time-02");
            this.exceptionNames.Add("Coordinated Universal Time-03");
            this.exceptionNames.Add("Coordinated Universal Time-04");
            this.exceptionNames.Add("Coordinated Universal Time-05");
            this.exceptionNames.Add("Coordinated Universal Time-06");
            this.exceptionNames.Add("Coordinated Universal Time-07");
            this.exceptionNames.Add("Coordinated Universal Time-08");
            this.exceptionNames.Add("Coordinated Universal Time-09");
            this.exceptionNames.Add("Coordinated Universal Time-10");
            this.exceptionNames.Add("Coordinated Universal Time-11");
            this.exceptionNames.Add("Coordinated Universal Time-12");
            this.exceptionNames.Add("Coordinated Universal Time+01");
            this.exceptionNames.Add("Coordinated Universal Time+02");
            this.exceptionNames.Add("Coordinated Universal Time+03");
            this.exceptionNames.Add("Coordinated Universal Time+04");
            this.exceptionNames.Add("Coordinated Universal Time+05");
            this.exceptionNames.Add("Coordinated Universal Time+06");
            this.exceptionNames.Add("Coordinated Universal Time+07");
            this.exceptionNames.Add("Coordinated Universal Time+08");
            this.exceptionNames.Add("Coordinated Universal Time+09");
            this.exceptionNames.Add("Coordinated Universal Time+10");
            this.exceptionNames.Add("Coordinated Universal Time+11");
            this.exceptionNames.Add("Coordinated Universal Time+12");
            this.exceptionNames.Add("Coordinated Universal Time+13");
        }


        /// <summary>
        /// TimeZoneIndex is a HashTable of Geographic names against Time Zones.  For the purposes of this exercise, the Geographic locations are extracted 
        /// from the TimeZoneInfo display names.  
        /// </summary>
        private void CreateTimeZoneIndex()
        {
            // Parse times zones once, split strings and create an index.   
            System.Collections.ObjectModel.ReadOnlyCollection<TimeZoneInfo> zones = TimeZoneInfo.GetSystemTimeZones();
            this.timeZoneIndex = new System.Collections.Hashtable();

            foreach (TimeZoneInfo zone in zones)
            {
                string[] cities = zone.DisplayName.Split(',');
                foreach (string city in cities)
                {
                    // Strip out (UTC-xx:xx) using Regular Expressions
                    string regex = @"^\(UTC[-+]\d\d:\d\d\)";
                    string cleanCity = Regex.Replace(city, regex, String.Empty);
                    cleanCity = cleanCity.Trim();

                    // Check if in Exceptions list, if its not, then it can be added to the TimeZoneList
                    int count = this.exceptionNames.Count(s => s == cleanCity);
                    if (count == 0)
                    {
                        try
                        {
                            this.timeZoneIndex.Add(cleanCity, zone.Id);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception in: TimeZoneManager::CreateTimeZoneIndex()  {e.Message}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Lookup the TimeZone from the entered location.  Returns null if no timezone is found. 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public TimeZoneInfo FindTimeZone(string location)
        {
            string tzID = (string)this.timeZoneIndex[location];

            if (!string.IsNullOrEmpty(tzID))
            {
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(tzID);
                return tzi;
            }
            else
            {
                return null;
            }
        }
    }
}
