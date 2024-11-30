using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Enums;
using TechnologieSiecioweLibrary.Interfaces;

namespace TechnologieSiecioweLibrary.Controllers
{
    public class MsgController : IController
    {
        public Task<string> ProcessData(string json, Method method)
        {
            throw new NotImplementedException();
        }
    }
}
