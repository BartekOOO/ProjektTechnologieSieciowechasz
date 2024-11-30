using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary;
using TechnologieSiecioweLibrary.Enums;
using TechnologieSiecioweLibrary.Models;

namespace Playground
{
    internal class Program
    {
        static void Main(string[] args)
        {


            Client client = new Client();
            client.Connect("localhost", 5001);

            Console.WriteLine(Kodek.Decrypt("L7k8GNql6UAg6UmtuUfytWtE/z\u002BIltFwWWX2AXx8eoA1r1GBY9w/1CA9ByrMIMCf"));

            while (true)
            {
                string wiadomosc = Console.ReadLine();
                Message message = new Message();
                message.MessageText = wiadomosc;
                RequestData<Message> request = new RequestData<Message>();
                request.Data = message;
                request.Method = Method.Post;

                client.SendData(request.GetJSONBody());
                try
                {
                    Console.WriteLine(client.responses.Last());
                }
                catch
                {

                }
            }


            //RequestData<User> requestData = new RequestData<User>();
            //requestData.Data = new User() { UserName = Console.ReadLine() };
            //requestData.Data.Password = Console.ReadLine();
            //requestData.Method = Method.Login;


            //client.SendData(requestData.GetJSONBody());

            //Thread.Sleep(1000);



            //ResponseData<Token> response = new ResponseData<Token>();
            //response.ReadDataFromJSON(client.responses.FirstOrDefault());

            //Token token = response.Data;

            //Console.WriteLine(Kodek.Decrypt(token.TokenBW));

            //RequestData<User> updateUser = new RequestData<User>();
            //updateUser.Token = token;
            //updateUser.Method = Method.Put;
            //updateUser.Data = new User();
            //updateUser.Data.Email = "testowe gówno";
            //updateUser.Data.Password = "password";
            //updateUser.Data.UserName = "user1";

            //client.SendData(updateUser.GetJSONBody());

            //Thread.Sleep(1000);

            //Console.WriteLine(client.responses.Last());

            //client.Disconnect();

            //Console.ReadKey();
        }
    }
}
