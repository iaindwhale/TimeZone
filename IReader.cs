using System;
using System.Collections.Generic;

namespace Timezone
{
    interface IReader
    {
        List<Tuple<string, string>> Read();
    }
}
