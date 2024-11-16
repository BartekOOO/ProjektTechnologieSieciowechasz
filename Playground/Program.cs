using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Enums;
using TechnologieSiecioweLibrary.Models;

namespace Playground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User(1,"user1","password","email");
            //Console.WriteLine(user.GetJSONBody());
            RequestData<User> requestData = new RequestData<User>();
            requestData.Data = user;
            requestData.Method = Method.Post;
            requestData.Token = new Token();
            Console.WriteLine(requestData.GetJSONBody());
            

            Client client = new Client("localhost",5001);
            string response = client.SendData(requestData);
            Console.WriteLine(response);

            Console.ReadKey();
        }
    }
}
