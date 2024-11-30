using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
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
                    ResponseData<Message> responseData = new ResponseData<Message>();
                    responseData.Data = new Message();
                    responseData.Data.MessageText = "Zajebiscie";
                    responseData.ResponseCode = ResponseCode.OK;
                    return responseData.GetJSONBody();
                default:

                    break;

                   
            }
            return "";
        }
    }
}
