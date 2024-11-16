using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnologieSiecioweLibrary
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            try
            {
                var field = value.GetType().GetField(value.ToString());
                var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                return attribute == null ? value.ToString() : attribute.Description;
            }   
            catch
            {
                return "";
            }
        }
    }
}
