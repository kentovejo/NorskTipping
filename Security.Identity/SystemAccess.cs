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
