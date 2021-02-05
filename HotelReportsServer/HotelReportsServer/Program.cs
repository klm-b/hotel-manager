using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HotelReportsServer
{
    class Program
    {
        private static Controllers.TcpChatServer _server;

        static void Main(string[] args)
        {
            Console.Title = "Сервер. Отчеты. Demo version";
            Console.SetWindowSize(140, 30);

            try
            {
                // создание контроллера сервера
                _server = new Controllers.TcpChatServer();

                // запуск обработки в новом потоке
                new Thread(_server.StartListening).Start();

                Console.ReadKey(true);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}\n");
            }
            finally
            {
                _server.Close();
            }
        }
    }
}
