using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Enums;
using TechnologieSiecioweLibrary.Models;

namespace Playground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User();

            Console.WriteLine("Podaj login");
            user.UserName = Console.ReadLine();
            Console.WriteLine("Podaj hasło");
            user.Password = Console.ReadLine();

            Client client = new Client("localhost",5001);
            client.Connect();
            RequestData<User> requestData = new RequestData<User>();
            requestData.Data = user;
            requestData.Method = Method.Login;
            client.SendData(requestData);


            Thread.Sleep(5000);
            ResponseData<Token> responseData = new ResponseData<Token>();
            responseData.ReadDataFromJSON(client.GetLastMessage());
            Console.WriteLine(responseData.GetJSONBody());
            Console.ReadKey();
        }
    }
}
