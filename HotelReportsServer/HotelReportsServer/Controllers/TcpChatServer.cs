using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReportsServer.Controllers
{
    class TcpChatServer
    {
        private static TcpListener _tcpListener;

        // запуск прослушивания 
        public void StartListening()
        {
            try
            {
                int port = 8888;
                _tcpListener = new TcpListener(IPAddress.Any, port);
                _tcpListener.Start();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Сервер запущен(порт {port}).");
                Console.ResetColor();

                Console.WriteLine($"[{DateTime.Now:t}] Ожидаются подключения...");

                // обработка подключений
                while (true)
                {
                    // получение подключения
                    var tcpClient = _tcpListener.AcceptTcpClient();

                    // создание контроллера
                    var clientConnection = new ClientConnection(tcpClient, this);

                    // запуск обработки подлкючения в отдельной задаче
                    Task.Run(clientConnection.Process);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Close();
            }
        }

        // отключение сервера
        public void Close()
        {
            _tcpListener.Stop();
        }
    }
}
