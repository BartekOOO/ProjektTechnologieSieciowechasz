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

                Token token;
                try
                {
                    token = JsonSerializer.Deserialize<RequestData<Token>>(json).Token;

                    if (method == Method.Put)
                    {
                        string test = Kodek.Decrypt(token.TokenBW);
                    }
                }
                catch
                {
                    token = new Token();
                }

                switch (className)
                {
                    case "User":
                        return await new UserController().ProcessData(jsonData.ToString(),method,token);
                    case "Message":
                        return await new MsgController().ProcessData(jsonData.ToString(),method, token);


                }
            }


            return "";
        }
    }
}
