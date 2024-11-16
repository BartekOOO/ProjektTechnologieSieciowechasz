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
    public class ResponseData<T>
    {
        public ResponseCode ResponseCode { get; set; }

        [JsonInclude]
        private string ClassName => typeof(T).Name;

        [JsonInclude]
        public T Data { get; set; }

        public ResponseData()
        {

        }

        public string GetJSONBody()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
