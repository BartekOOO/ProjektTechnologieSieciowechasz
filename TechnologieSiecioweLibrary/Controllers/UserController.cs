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

                    if (String.IsNullOrEmpty(newUser.UserName) || String.IsNullOrEmpty(newUser.Password) || newUser == null)
                    {
                        ResponseData<User> badResult = new ResponseData<User>();
                        badResult.ResponseCode = ResponseCode.BAD_REQUEST;
                        badResult.Data = null;
                        return JsonSerializer.Serialize(badResult);
                    }

                    DatabaseHelper.ExecuteStoredProcedure(
                        $"PROJEKT.InsertUser",newUser.GetInsertParameters(),new ConfigS());
                    result.ResponseCode = ResponseCode.CREATED;
                    result.Data = newUser;
                    return JsonSerializer.Serialize(result);
                case Method.Delete:
                    User deleteUser = JsonSerializer.Deserialize<User>(json);

                    await DatabaseHelper.ExecuteStoredProcedure(
                        $"PROJEKT.DeleteUser", deleteUser.GetDeleteParameters(), new ConfigS());

                    ResponseData<User> deleteResponse = new ResponseData<User>();
                    deleteResponse.ResponseCode = ResponseCode.ACCEPTED;
                    deleteResponse.Data = null;

                    return JsonSerializer.Serialize(deleteResponse);
                case Method.Put:
                    User editUser = JsonSerializer.Deserialize<User>(json);

                    await DatabaseHelper.ExecuteStoredProcedure(
                        "PROJEKT.InsertUser", editUser.GetUpdateParameters(),new ConfigS());

                    ResponseData<User> editResponse = new ResponseData<User>();
                    editResponse.ResponseCode = ResponseCode.ACCEPTED;
                    editResponse.Data = editUser;

                    return JsonSerializer.Serialize(editResponse);
                case Method.Logut:



                    break;

                case Method.Login:
                    User loginRequest = JsonSerializer.Deserialize<User>(json);
                    ResponseData<Token> loginResponse = new ResponseData<Token>();


                    DataTable dtl = await DatabaseHelper.ExecuteStoredProcedureWithResult(
                        $"PROJEKT.UserExists", new Dictionary<string, object> { { "@UserName", loginRequest.UserName }, { "@Password", Kodek.Encrypt(loginRequest.Password) } }, new ConfigS());

                    

                    if (dtl.Rows.Count == 0)
                    {
                        ResponseData<Token> badResult = new ResponseData<Token>();
                        badResult.ResponseCode = ResponseCode.BAD_REQUEST;
                        badResult.Data = null;
                        return JsonSerializer.Serialize(badResult);
                    }

                    DataRow row = dtl.Rows[0];
                    int id = row.Field<int>("PUS_Id");


                    Token token = new Token();
                    token.TokenBW = Kodek.Encrypt(
                        $"TOKENBW;ABC;{id};ABC;{DateTime.Now.AddMinutes(15)};ABC;");

                    loginResponse.Data = token;
                    loginResponse.ResponseCode = ResponseCode.OK;
                    return JsonSerializer.Serialize(loginResponse);
                    


                default:
                    break;
            }

            ResponseData<User> badRequest = new ResponseData<User>();
            badRequest.ResponseCode = ResponseCode.BAD_REQUEST;
            return JsonSerializer.Serialize(badRequest);
        }
    }
}
