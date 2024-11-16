using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnologieSiecioweLibrary.Interfaces
{
    public interface IConfig
    {
        string SQL_ServerName { get; set; }
        string SQL_DatabaseName { get; set; }
        string SQL_Login { get; set; }
        string SQL_EncryptedPassword { get; set; }
        bool SQL_UseIntegratedSecurity { get; set; }
    }
}
