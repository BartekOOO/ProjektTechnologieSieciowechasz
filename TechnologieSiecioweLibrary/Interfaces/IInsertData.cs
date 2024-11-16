using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnologieSiecioweLibrary.Interfaces
{
    public interface IInsertData
    {
        Dictionary<string, object> GetInsertParameters();
    }
}
