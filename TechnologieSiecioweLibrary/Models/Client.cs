using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Interfaces;

namespace TechnologieSiecioweLibrary.Models
{
    public class Client
    {
        private readonly string _serverAddress;
        private readonly int _port;

        public Client(string serverAddress, int port)
        {
            _serverAddress = serverAddress;
            _port = port;
        }

        public string SendData<T>(RequestData<T> requestData) where T : IData
        {
            if (string.IsNullOrEmpty(requestData.GetJSONBody()))
                throw new ArgumentException("Dane JSON nie mogą być puste.");

            try
            {
                using (TcpClient client = new TcpClient(_serverAddress, _port))
                using (NetworkStream stream = client.GetStream())
                {
                    Console.WriteLine("Połączono z serwerem.");

                   
                    byte[] jsonBytes = Encoding.UTF8.GetBytes(requestData.GetJSONBody()+";;;");


                    Console.WriteLine("Wysyłanie danych...");
                    stream.Write(jsonBytes, 0, jsonBytes.Length);
                    stream.Flush();
                    Console.WriteLine("Dane zostały wysłane.");


                    StringBuilder responseBuilder = new StringBuilder();
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        responseBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                        if (responseBuilder.ToString().EndsWith(";;;"))
                        {
                            break;
                        }
                    }

                    string response = responseBuilder.ToString().TrimEnd(';');
                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                throw;
            }
        }
    }
}
