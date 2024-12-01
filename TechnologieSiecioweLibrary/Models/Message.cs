using System;
using System.Collections.Generic;
using System.Data;
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

        public string SenderName { get; set; } = "";
        

        public string GetClassName()
        {
            return "Message";
        }

        public Message()
        {

        }

        public Message(DataRow row)
        {
            this.Id = row.Field<int>("PMS_Id");
            this.SenderId = row.Field<int>("PMS_SenderId");
            this.ReceiverId = row.Field<int>("PMS_ReceiverId");
            this.MessageText = row.Field<string>("PMS_Message");
            this.Date = row.Field<DateTime>("PMS_Date");
            this.SenderName = row.Field<string>("PUS_UserName");
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
