using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Interfaces;

namespace TechnologieSiecioweLibrary.Models
{
    public class Client
    {
        private readonly string _serverAddress;
        private readonly int _port;
        private TcpClient _client;
        private NetworkStream _stream;
        private Thread _receiveThread;

        private string _lastMessage;
        private readonly object _messageLock = new object();

        public Client(string serverAddress, int port)
        {
            _serverAddress = serverAddress;
            _port = port;
        }

        public string GetLastMessage()
        {
            lock (_messageLock)
            {
                return _lastMessage;
            }
        }

        public void Connect()
        {
            try
            {
                _client = new TcpClient(_serverAddress, _port);
                _stream = _client.GetStream();
                Console.WriteLine("Połączono z serwerem.");


                _receiveThread = new Thread(ReceiveMessages);
                _receiveThread.IsBackground = true;
                _receiveThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd połączenia: {ex.Message}");
            }
        }

        public void SendData<T>(RequestData<T> requestData) where T : IData
        {
            try
            {
                if (_client == null || !_client.Connected)
                {
                    Console.WriteLine("Nie połączono z serwerem.");
                    return;
                }

                byte[] messageBytes = Encoding.UTF8.GetBytes(requestData.GetJSONBody() + ";;;");
                _stream.Write(messageBytes, 0, messageBytes.Length);
                _stream.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd wysyłania danych: {ex.Message}");
            }
        }

        private void ReceiveMessages()
        {
            try
            {
                byte[] buffer = new byte[1024];
                StringBuilder messageBuilder = new StringBuilder();

                while (true)
                {
                    int bytesRead = _stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        messageBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                        if (messageBuilder.ToString().EndsWith(";;;"))
                        {
                            string completeMessage = messageBuilder.ToString().TrimEnd(';');
                            lock (_messageLock)
                            {
                                _lastMessage = completeMessage;
                            }
                            messageBuilder.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd odbierania wiadomości: {ex.Message}");
            }
        }

        public void Disconnect()
        {
            try
            {
                _receiveThread?.Abort();
                _stream?.Close();
                _client?.Close();
                Console.WriteLine("Rozłączono z serwerem.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas rozłączania: {ex.Message}");
            }
        }
    }
}
