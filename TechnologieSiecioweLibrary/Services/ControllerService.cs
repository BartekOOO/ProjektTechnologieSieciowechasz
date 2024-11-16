using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Controllers;
using TechnologieSiecioweLibrary.Enums;
using TechnologieSiecioweLibrary.Interfaces;
using TechnologieSiecioweLibrary.Models;

namespace TechnologieSiecioweLibrary.Services
{
    public class ControllerService
    {
        public static async Task<string> ProcessData(string json)
        {
            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                string className = doc.RootElement.GetProperty("ClassName").GetString();
                object jsonData = doc.RootElement.GetProperty("Data");
                Method method = (Method)doc.RootElement.GetProperty("Method").GetInt32();
                
                switch (className)
                {
                    case "User":
                        return new UserController().ProcessData(jsonData.ToString(),method).Result;
                    case "Token":
                        return new TokenController().ProcessData(jsonData.ToString(),method).Result;
                }
            }


            return "";
        }
    }
}
