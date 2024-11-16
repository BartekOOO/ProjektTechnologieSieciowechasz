using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Enums;
using TechnologieSiecioweLibrary.Interfaces;
using TechnologieSiecioweLibrary.Models;

namespace TechnologieSiecioweLibrary.Controllers
{
    public class UserController : IController
    {
        public async Task<string> ProcessData(string json,Method method)
        {
            ResponseData<User> result = new ResponseData<User>();

            switch (method)
            {
                case Method.Post:
                    User newUser = JsonSerializer.Deserialize<User>(json);

                    DataTable dt = await DatabaseHelper.ExecuteStoredProcedureWithResult(
                        $"PROJEKT.UserExists", new Dictionary<string, object> { { "@UserName",newUser.UserName } },new ConfigS());

                    if (dt.Rows.Count > 0)
                    {
                        ResponseData<User> badResult = new ResponseData<User>();
                        badResult.ResponseCode = ResponseCode.USER_ALREADY_EXISTS;
                        badResult.Data = null;
                        return JsonSerializer.Serialize(badResult);
                    }

                    DatabaseHelper.ExecuteStoredProcedure(
                        $"PROJEKT.InsertUser",newUser.GetInsertParameters(),new ConfigS());
                    result.ResponseCode = ResponseCode.CREATED;
                    result.Data = newUser;
                    break;
                default:
                    return JsonSerializer.Serialize(new ResponseData<User>());
                    break;
            }


            return JsonSerializer.Serialize(result);
        }
    }
}
