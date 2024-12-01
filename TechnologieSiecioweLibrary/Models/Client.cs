using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Models;

public class Client
{
    private TcpClient _tcpClient;
    private NetworkStream _stream;
    private StreamReader _reader;
    private StreamWriter _writer;
    private Thread _receiveThread;
    public List<string> responses;

    public Action<string> NewData;

    public bool IsConnected { get; private set; } = false;

    public Client()
    {
        _tcpClient = new TcpClient();
        responses = new List<string>();
    }

    public void Connect(string serverAddress, int port)
    {
        try
        {
            _tcpClient.Connect(serverAddress, port);
            _stream = _tcpClient.GetStream();
            _reader = new StreamReader(_stream);
            _writer = new StreamWriter(_stream) { AutoFlush = true };

            IsConnected = true;

            _receiveThread = new Thread(ReceiveMessages);
            _receiveThread.Start();

            Console.WriteLine("Połączono z serwerem.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Błąd połączenia: " + ex.Message);
            IsConnected = false;
        }
    }

    public void SendData(string data)
    {
        if (IsConnected)
        {
            try
            {
                _writer.WriteLine(data);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas wysyłania wiadomości: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Brak połączenia z serwerem.");
        }
    }

    
    private void ReceiveMessages()
    {
        try
        {
            while (IsConnected)
            {
                string message = _reader.ReadLine();
                if (message != null)
                {
                    responses.Add(message);
                    NewData?.Invoke(message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Błąd odbierania wiadomości: " + ex.Message);
        }
        finally
        {
            Disconnect();
        }
    }

    
    public void Disconnect()
    {
        if (IsConnected)
        {
            _reader.Close();
            _writer.Close();
            _stream.Close();
            _tcpClient.Close();

            IsConnected = false;
            Console.WriteLine("Połączenie zakończone.");
        }
        else
        {
            Console.WriteLine("Brak aktywnego połączenia.");
        }
    }

    public Task<string> WaitForResponseAsync()
    {
        var tcs = new TaskCompletionSource<string>();

        Task.Run(() =>
        {
            while (!responses.Any()) 
            {
                Task.Delay(10).Wait();
            }
            tcs.SetResult(responses.Last());
        });

        return tcs.Task;
    }
}
