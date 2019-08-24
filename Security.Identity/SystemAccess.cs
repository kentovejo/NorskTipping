using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;

namespace Security.Identity
{
    public class SystemAccess
    {
#if DEBUG
        public static bool IsAuthenticated => true;
#else
        public static bool IsAuthenticated => ApplicationContext.User.Identity.IsAuthenticated;
#endif
    }
}
