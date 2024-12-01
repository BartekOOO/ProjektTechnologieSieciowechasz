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
        public async Task<string> ProcessData(string json, Method method, Token token)
        {
            ResponseData<User> result = new ResponseData<User>();

            try
            {
                switch (method)
                {
                    case Method.Post:
                        User newUser = JsonSerializer.Deserialize<User>(json);

                        DataTable dt = await DatabaseHelper.ExecuteStoredProcedureWithResult(
                            $"PROJEKT.UserExists", new Dictionary<string, object> { { "@UserName", newUser.UserName } }, new ConfigS());

                        if (dt.Rows.Count > 0)
                        {
                            ResponseData<User> badResult = new ResponseData<User>
                            {
                                ResponseCode = ResponseCode.USER_ALREADY_EXISTS,
                                Data = null
                            };
                            return JsonSerializer.Serialize(badResult);
                        }

                        if (string.IsNullOrEmpty(newUser.UserName) || string.IsNullOrEmpty(newUser.Password) || newUser == null)
                        {
                            ResponseData<User> badResult = new ResponseData<User>
                            {
                                ResponseCode = ResponseCode.BAD_REQUEST,
                                Data = null
                            };
                            return JsonSerializer.Serialize(badResult);
                        }

                        await DatabaseHelper.ExecuteStoredProcedure(
                            $"PROJEKT.InsertUser", newUser.GetInsertParameters(), new ConfigS());

                        result.ResponseCode = ResponseCode.CREATED;
                        result.Data = newUser;
                        return JsonSerializer.Serialize(result);

                    case Method.Delete:
                        User deleteUser = JsonSerializer.Deserialize<User>(json);

                        await DatabaseHelper.ExecuteStoredProcedure(
                            $"PROJEKT.DeleteUser", deleteUser.GetDeleteParameters(), new ConfigS());

                        ResponseData<User> deleteResponse = new ResponseData<User>
                        {
                            ResponseCode = ResponseCode.ACCEPTED,
                            Data = null
                        };
                        return JsonSerializer.Serialize(deleteResponse);

                    case Method.Put:
                        if (!token.CheckToken())
                        {
                            ResponseData<User> cannontEditUser = new ResponseData<User>
                            {
                                ResponseCode = ResponseCode.UNAUTHORIZED,
                                Data = null
                            };
                            return JsonSerializer.Serialize(cannontEditUser);
                        }

                        User editUser = JsonSerializer.Deserialize<User>(json);
                        editUser.Id = token.GetTokenData().Item2;
                        await DatabaseHelper.ExecuteStoredProcedure(
                            "PROJEKT.UpdateUser", editUser.GetUpdateParameters(), new ConfigS());

                        ResponseData<User> editResponse = new ResponseData<User>
                        {
                            ResponseCode = ResponseCode.ACCEPTED,
                            Data = editUser
                        };

                        return JsonSerializer.Serialize(editResponse);

                    case Method.Login:
                        User loginRequest = JsonSerializer.Deserialize<User>(json);
                        ResponseData<Token> loginResponse = new ResponseData<Token>();

                        DataTable dtl = await DatabaseHelper.ExecuteStoredProcedureWithResult(
                            $"PROJEKT.UserExists", new Dictionary<string, object>
                            {
                                { "@UserName", loginRequest.UserName },
                                { "@Password", Kodek.Encrypt(loginRequest.Password) }
                            }, new ConfigS());


                        if (dtl.Rows.Count == 0)
                        {
                            ResponseData<Token> badResult = new ResponseData<Token>
                            {
                                ResponseCode = ResponseCode.BAD_REQUEST,
                                Data = null
                            };
                            return JsonSerializer.Serialize(badResult);
                        }

                        DataRow row = dtl.Rows[0];
                        int id = row.Field<int>("PUS_Id");
                        string login = row.Field<string>("PUS_UserName");

                        Token newToken = new Token(id, 15, login);

                        await DatabaseHelper.ExecuteStoredProcedure(
                            $"PROJEKT.UpdateUserLastLogin",
                            new Dictionary<string, object> { { "@Id", id } }, new ConfigS());


                        loginResponse.Data = newToken;
                        loginResponse.ResponseCode = ResponseCode.OK;
                        return JsonSerializer.Serialize(loginResponse);
                    case Method.Get:
                        if (!token.CheckToken())
                        {
                            ResponseData<User> cannontEditUser = new ResponseData<User>
                            {
                                ResponseCode = ResponseCode.UNAUTHORIZED,
                                Data = null
                            };
                            return JsonSerializer.Serialize(cannontEditUser);
                        }
                        User userDetails = new User();

                        int userId = token.GetTokenData().Item2;

                        DataTable dtud = await DatabaseHelper.ExecuteStoredProcedureWithResult(
                            $"PROJEKT.GetUsers",new Dictionary<string, object> { { "@Id", userId } }, new ConfigS());

                        userDetails = new User(dtud.Rows[0]);

                        ResponseData<User> response = new ResponseData<User>();
                        response.Data = userDetails;
                        response.ResponseCode = ResponseCode.OK;

                        return response.GetJSONBody();
                    default:
                        ResponseData<User> badRequest = new ResponseData<User>
                        {
                            ResponseCode = ResponseCode.BAD_REQUEST,
                            Data = null
                        };
                        return JsonSerializer.Serialize(badRequest);
                }
            }
            catch (Exception ex)
            {
                // Obsługa błędów
                ResponseData<User> errorResponse = new ResponseData<User>
                {
                    ResponseCode = ResponseCode.INTERNAL_SERVER_ERROR,
                    Data = null
                };
                Console.WriteLine($"Error: {ex.Message}");
                return JsonSerializer.Serialize(errorResponse);
            }
        }

    }
}
