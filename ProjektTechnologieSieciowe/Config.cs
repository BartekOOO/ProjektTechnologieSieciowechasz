using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Models;

namespace ProjektTechnologieSieciowe
{
    public static class Config
    {
        public static Token token { get; set; }
        public static int port { get; } = 5001;
        public static string host { get; } = "localhost";
    }
}
