using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Interfaces;

namespace TechnologieSiecioweLibrary.Models
{
    public class Message : IData
    {
        public string GetClassName()
        {
            return "Message";
        }

        public string GetJSONBody()
        {
            throw new NotImplementedException();
        }

        public void ReadDataFromJSON(string JSONBody)
        {
            throw new NotImplementedException();
        }
    }
}
