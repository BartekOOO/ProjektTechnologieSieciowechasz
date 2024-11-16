using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Interfaces;

namespace TechnologieSiecioweLibrary.Models
{
    public class ConfigS : IConfig
    {
        public string SQL_ServerName { get; set; }
        public string SQL_DatabaseName { get; set; }
        public string SQL_Login { get; set; }
        public string SQL_EncryptedPassword { get; set; }
        public bool SQL_UseIntegratedSecurity { get; set; }

        public ConfigS()
        {
            this.SQL_ServerName = "BartekLap";
            this.SQL_DatabaseName = "ProjektTechnologieSieciowe";
            this.SQL_Login = "test";
            this.SQL_EncryptedPassword = Kodek.Encrypt("test");
            this.SQL_UseIntegratedSecurity = false;   
        }

    }
}
