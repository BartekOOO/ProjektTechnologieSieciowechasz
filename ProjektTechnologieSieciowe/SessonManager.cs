using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTechnologieSieciowe
{
    public static class SessionManager
    {
        private static DateTime _lastActivityTime;
        public static DateTime? SessionExpiration { get; set; } = null;

        public static void UpdateActivity()
        {
            _lastActivityTime = DateTime.Now;
        }

        public static bool IsSessionValid()
        {
            if (SessionExpiration==null) return true;
            return SessionExpiration > DateTime.Now;
        }

        public static TimeSpan GetRemainingTime()
        {
            if (SessionExpiration == null)
            {
                return TimeSpan.Zero;
            }

            return SessionExpiration.Value - DateTime.Now;
        }
    }

}
