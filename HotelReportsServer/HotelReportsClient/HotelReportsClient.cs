using HotelReportsModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HotelReportsClient
{
    public class HotelReportsClient
    {
        private readonly IPEndPoint _ipEndPoint;
        private TcpClient _client;
        private NetworkStream _stream;

        public HotelReportsClient(IPEndPoint ipEndPoint)
        {
            _ipEndPoint = ipEndPoint;
            _client = new TcpClient();

            // подключение к серверу и получения потока для обмена данными
            _client.Connect(_ipEndPoint);
            _stream = _client.GetStream();
        }

        // отправка отчета
        public void SendReport(HotelReport hotelReport, string reportFileName)
        {
            try
            {
                var writer = new BinaryWriter(_stream);

                // сериализация объекта в json строку и кодирование ее массив байт для отправки
                byte[] data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(hotelReport, Formatting.Indented));

                writer.Write("@@@report\n");
                writer.Write(reportFileName);
                writer.Write(data.Length);
                writer.Write(data);
            }
            catch (Exception)
            {
                _stream?.Close();
                _client?.Close();
                throw;
            }
        }

        // отправка команды
        public void SendCommand(string command)
        {
            if (string.IsNullOrEmpty(command)) return;

            try
            {
                var writer = new BinaryWriter(_stream);
                writer.Write(command);
            }
            catch (Exception)
            {
                _stream?.Close();
                _client?.Close();
                throw;
            }
        }
    }
}
