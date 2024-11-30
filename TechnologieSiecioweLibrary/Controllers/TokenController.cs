using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Enums;
using TechnologieSiecioweLibrary.Interfaces;
using TechnologieSiecioweLibrary.Models;

namespace TechnologieSiecioweLibrary.Controllers
{
    public class TokenController : IController
    {
        public Task<string> ProcessData(string json, Method method, Token token)
        {
            throw new NotImplementedException();
        }
    }
}
