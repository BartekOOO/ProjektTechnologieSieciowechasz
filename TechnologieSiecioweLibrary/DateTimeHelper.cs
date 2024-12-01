using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnologieSiecioweLibrary
{
    public class DateTimeHelper
    {
        public static string FormatDateTime(DateTime datetime)
        {
            return datetime.ToString("dd MMMM yyyy, HH:mm", new CultureInfo("pl-PL"));
        }

        public static string FormatDateTime2(DateTime datetime)
        {
            return datetime.ToString("HH:mm", new CultureInfo("pl-PL"));
        }

        public static string FormatDateTime3(TimeSpan datetime)
        {
            if (datetime.Minutes > 0)
            {
                return datetime.ToString(@"mm\:ss", new CultureInfo("pl-PL"));
            }
            else
            {
                return datetime.Seconds.ToString("00", new CultureInfo("pl-PL"));
            }
        }

    }
}
