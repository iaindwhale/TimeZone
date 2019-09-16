using System;

namespace Timezone
{
    interface ITimeZoneManager
    {
        TimeZoneInfo FindTimeZone(string location);
    }
}
