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
            get; set;
        }

        public Token()
        {
            //TokenBW = $"TOKENBW;ABC;{0};ABC;{DateTime.Now.AddMinutes(-15000)};ABC;";
            TokenBW = Kodek.Encrypt(
                        $"TOKENBW;ABC;{0};ABC;{DateTime.Now.AddMinutes(-15000)};ABC;Unauthorized");
        }
        public Token(int id, int time,string login)
        {

            TokenBW = Kodek.Encrypt(
                        $"TOKENBW;ABC;{id};ABC;{DateTime.Now.AddMinutes(15)};ABC;{login}");

            //TokenBW = $"TOKENBW;ABC;{id};ABC;{DateTime.Now.AddMinutes(15)};ABC;";
        }

        public string GetClassName()
        {
            return "Token";
        }

        public string GetJSONBody()
        {
            return JsonSerializer.Serialize(this);
        }

        public bool CheckToken()
        {
            return this.GetTokenData().Item1 > DateTime.Now;
        }

        public Tuple<DateTime, int, string> GetTokenData()
        {
            string decryptedData = "";

            //decryptedData = Kodek.Encrypt(decryptedData);

            decryptedData = Kodek.Decrypt(TokenBW);



            string[] dataParts = decryptedData.
                Split(new string[] { ";ABC;" }, StringSplitOptions.None);

            int userId = int.Parse(dataParts[1]);
            DateTime expirationDate = DateTime.Parse(dataParts[2]);
            string login = dataParts[3];
            return new Tuple<DateTime, int, string>(expirationDate, userId, login);
        }

        public void ReadDataFromJSON(string JSONBody)
        {
            Token result = JsonSerializer.Deserialize<Token>(JSONBody);
            this.TokenBW = result.TokenBW;
        }
    }
}
