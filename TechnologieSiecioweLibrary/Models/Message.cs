using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Interfaces;

namespace TechnologieSiecioweLibrary.Models
{
    public class Message : IData, IInsertData
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageText { get; set; }
        public DateTime Date { get; set; }


        public string GetClassName()
        {
            return "Message";
        }

        public Dictionary<string, object> GetInsertParameters()
        {
            Dictionary<string,object> parameters = new Dictionary<string,object>(); ;

            parameters.Add("@ReceiverId", ReceiverId);
            parameters.Add("@SenderId", SenderId);
            parameters.Add("@Message", MessageText);

            return parameters;
        }

        public string GetJSONBody()
        {
            return JsonSerializer.Serialize(this);
        }

        public void ReadDataFromJSON(string JSONBody)
        {
            Message tmpMessage = JsonSerializer.Deserialize<Message>(JSONBody);
            this.Id = tmpMessage.Id;
            this.SenderId = tmpMessage.SenderId;
            this.ReceiverId = tmpMessage.ReceiverId;
            this.Date = tmpMessage.Date;
            this.MessageText = tmpMessage.MessageText;
        }
    }
}
