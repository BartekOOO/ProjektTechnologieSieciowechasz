using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Interfaces;

namespace TechnologieSiecioweLibrary.Models
{
    public class User : IInsertData, IUpdateData, IDeleteData, IData
    {
        public int Id { get; set; } = 0;
        public string UserName { get; set; } = "";
        private string EncryptedPassword { get; set; } = "";
        public string Password
        {
            get
            {
                return Kodek.Decrypt(EncryptedPassword);
            }
            set
            {
                EncryptedPassword = Kodek.Encrypt(value);
            }
        }
        public string Email { get; set; } = "";

        public User()
        {

        }

        public User(DataRow row)
        {
            this.Id = row.Field<int>("PUS_Id");
            this.Email = row.Field<string>("PUS_EMAIL");
            this.UserName = row.Field<string>("PUS_UserName");
            this.EncryptedPassword = row.Field<string>("PUS_Password");
        }

        public User(int id, string userName, string password, string email)
        {
            this.Id = id;
            this.Email = email;
            this.UserName = userName;
            this.Password = password;
        }

        public Dictionary<string, object> GetInsertParameters()
        {
            Dictionary<string, object> paramsDict = new Dictionary<string, object>();
            paramsDict.Add("@UserName", this.UserName);
            paramsDict.Add("@Email", this.Email);
            paramsDict.Add("@Password", this.EncryptedPassword);
            return paramsDict;
        }

        public Dictionary<string, object> GetUpdateParameters()
        {
            Dictionary<string, object> paramsDict = this.GetInsertParameters();
            paramsDict.Add("@Id", this.Id);
            return paramsDict;
        }

        public Dictionary<string, object> GetDeleteParameters()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("@Id", this.Id);
            return result;
        }

        public string GetClassName()
        {
            return "User";
        }

        public string GetJSONBody()
        {
            User tmpUser = this;
            tmpUser.Password = this.EncryptedPassword;
            return JsonSerializer.Serialize(tmpUser);
        }

        public void ReadDataFromJSON(string JSONBody)
        {
            User result = JsonSerializer.Deserialize<User>(JSONBody); ;

            this.Id = result.Id;
            this.Email = result.Email;
            this.UserName = result.UserName;
            this.EncryptedPassword = result.EncryptedPassword;
        }
    }
}
