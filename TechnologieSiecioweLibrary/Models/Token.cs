using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Interfaces;

namespace TechnologieSiecioweLibrary.Models
{
    public class Token : IData
    {
        public string TokenBW
        {
            get;set;
        }

        public string GetClassName()
        {
            return "Token";
        }

        public string GetJSONBody()
        {
            return JsonSerializer.Serialize(this);
        }

        public void ReadDataFromJSON(string JSONBody)
        {
            Token result = JsonSerializer.Deserialize<Token>(JSONBody);
            this.TokenBW = result.TokenBW;
        }
    }
}
