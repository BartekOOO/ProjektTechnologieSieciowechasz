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
    }
}
