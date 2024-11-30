using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Services;

class Server
{
    private static List<NetworkStream> clients = new List<NetworkStream>();
    private static TcpListener listener;

    static void Main(string[] args)
    {
        int port = 5001;
        listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine("Serwer nasłuchuje na porcie " + port);

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Połączono z klientem.");
            Task.Run(() => HandleClient(client));
        }
    }

    static async Task HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        clients.Add(stream);

        StreamReader reader = new StreamReader(stream);
        StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

        try
        {
            while (true)
            {
                string message = await reader.ReadLineAsync();
                if (message == null) break;

                Console.WriteLine("Otrzymano wiadomość: " + message);

                string result = await ControllerService.ProcessData(message);

                Console.WriteLine("Wysłano wiadomość: "+result);

                await writer.WriteLineAsync(result);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Błąd: " + ex.Message);
        }
        finally
        {
            clients.Remove(stream);
            client.Close();
        }
    }
}
