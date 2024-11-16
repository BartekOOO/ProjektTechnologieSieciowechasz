using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Enums;
using TechnologieSiecioweLibrary.Interfaces;

namespace TechnologieSiecioweLibrary.Models
{
    public class RequestData<T>
    {
        public Method Method { get; set; }
        public Token Token { get; set; }

        [JsonInclude]
        private string ClassName => typeof(T).Name;

        public T Data { get; set; }



        public string GetJSONBody()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
