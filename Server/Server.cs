using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.Json;
    using TechnologieSiecioweLibrary.Interfaces;
    using TechnologieSiecioweLibrary.Models;
    using TechnologieSiecioweLibrary.Services;

    class Server
    {
        static async Task Main(string[] args)
        {
            const int port = 5001;
            TcpListener listener = new TcpListener(IPAddress.Any, port);

            try
            {
                listener.Start();
                Console.WriteLine($"Serwer nasłuchuje na porcie {port}...");

                while (true)
                {
                    using (TcpClient client = listener.AcceptTcpClient())
                    using (NetworkStream stream = client.GetStream())
                    {
                        Console.WriteLine("Połączono z klientem.");
                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        StringBuilder jsonBuilder = new StringBuilder();

                        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            jsonBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                            if (jsonBuilder.ToString().Trim().EndsWith(";;;"))
                                break;
                        }

                        string receivedJson = jsonBuilder.ToString().TrimEnd(';').TrimEnd(';').TrimEnd(';');
                        await Console.Out.WriteLineAsync("Odebrano:"+receivedJson);



                        string response = await ControllerService.ProcessData(receivedJson);

                    
                        byte[] responseBytes = Encoding.UTF8.GetBytes(response+";;;");
                        stream.Write(responseBytes, 0, responseBytes.Length);
                        stream.Flush();
                        Console.WriteLine("Odpowiedź została wysłana.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            finally
            {
                listener.Stop();
            }
        }
    }

}
