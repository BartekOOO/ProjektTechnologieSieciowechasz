using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Enums;
using TechnologieSiecioweLibrary.Interfaces;
using TechnologieSiecioweLibrary.Models;

namespace TechnologieSiecioweLibrary.Controllers
{
    public class MsgController : IController
    {
        public async Task<string> ProcessData(string json, Method method, Token token)
        {
            switch (method)
            {
                case Method.Post:
                    if (!token.CheckToken())
                    {
                        ResponseData<List<Message>> notAuthorizedRsponse = new ResponseData<List<Message>>();
                        notAuthorizedRsponse.Data = null;
                        notAuthorizedRsponse.ResponseCode = ResponseCode.UNAUTHORIZED;
                        return JsonSerializer.Serialize(notAuthorizedRsponse);
                    }
                    ResponseData<Message> responseData = new ResponseData<Message>();

                    Message msgtosend = new Message();
                    msgtosend.ReadDataFromJSON(json);
                    msgtosend.SenderId = token.GetTokenData().Item2;
                    msgtosend.SenderName = token.GetTokenData().Item3;


                    await DatabaseHelper.ExecuteStoredProcedure(
                        $"PROJEKT.InsertMessage",
                        new Dictionary<string,object>()
                        {
                            { "@SenderId", msgtosend.SenderId },
                            { "@ReceiverId", msgtosend.ReceiverId },
                            { "@Message", msgtosend.MessageText }
                        },new ConfigS());


                    responseData.Data = msgtosend;
                    responseData.ResponseCode = ResponseCode.OK;
                    return responseData.GetJSONBody();
                case Method.List:
                    if (!token.CheckToken())
                    {
                        ResponseData<List<Message>> notAuthorizedRsponse = new ResponseData<List<Message>>();
                        notAuthorizedRsponse.Data = null;
                        notAuthorizedRsponse.ResponseCode = ResponseCode.UNAUTHORIZED;
                        return JsonSerializer.Serialize(notAuthorizedRsponse);
                    }

                    
                    Message msg = new Message();
                    msg.ReadDataFromJSON(json);
                    

                    DataTable messages = await DatabaseHelper.ExecuteStoredProcedureWithResult(
                        $"PROJEKT.GetMessages",
                        new Dictionary<string, object> { { "@SenderId", token.GetTokenData().Item2 }, { "@ReceiverId", msg.ReceiverId } },
                        new ConfigS());
                    List<Message> result = new List<Message>();
                    foreach(DataRow message in messages.Rows)
                    {
                        result.Add(new Message(message));
                    }
                    ResponseData<List<Message>> responseMessages = new ResponseData<List<Message>>();
                    responseMessages.Data = result;
                    responseMessages.ResponseCode = ResponseCode.OK;
                    return JsonSerializer.Serialize(responseMessages);


                default:

                    break;

                   
            }
            return "";
        }
    }
}
