using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Enums;
using TechnologieSiecioweLibrary.Models;

namespace TechnologieSiecioweLibrary.Interfaces
{
    public interface IController
    {
        Task<string> ProcessData(string json,Method method,Token token);
    }
}
