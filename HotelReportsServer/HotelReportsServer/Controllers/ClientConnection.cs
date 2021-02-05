using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using HotelReportsModel;


namespace HotelReportsServer.Controllers
{
    class ClientConnection
    {
        public NetworkStream Stream { get; private set; }

        private readonly TcpClient _client;


        public ClientConnection(TcpClient tcpClient, TcpChatServer server)
        {
            _client = tcpClient;
        }

        // обработка подключения
        public void Process()
        {
            try
            {
                Stream = _client.GetStream();
                var reader = new BinaryReader(Stream);

                // сообщение о входе пользователя
                Console.WriteLine($"[{DateTime.Now:t}] Новое подключение[{_client.Client.RemoteEndPoint}]. Идет обработка...");

                // работа с данными
                while (true)
                {
                    try
                    {
                        var head = reader.ReadString();
                        var command = head.Split('\n')[0];
                        switch (command)
                        {
                            // клиент вышел
                            case "@@@exit":
                                throw new Exception($"[{DateTime.Now:t}] Клиент [{_client.Client.RemoteEndPoint}] отключился.");

                            // получение отчета, проверка и сохранение его на сервере
                            case "@@@report":
                                {
                                    // получение названия файла
                                    string fileName = reader.ReadString();

                                    // получение размера файла
                                    int bytes = reader.ReadInt32();

                                    // чтение заданного кол-ва байтов из потока
                                    byte[] buf = reader.ReadBytes(bytes);

                                    // попытка получить данные
                                    HotelReport hotelReport;
                                    try
                                    {
                                        hotelReport = JsonConvert.DeserializeObject<HotelReport>(Encoding.Default.GetString(buf));
                                    }
                                    catch (JsonSerializationException)
                                    {
                                        Console.WriteLine($"[{DateTime.Now:t}] Ошибка. Файл \"{fileName}\" не является отчетом о работе гостиницы.");
                                        break;
                                    }

                                    // создание потока для записи файла
                                    Directory.CreateDirectory("App_Data");
                                    BinaryWriter bwr = new BinaryWriter(File.OpenWrite($"App_Data/{fileName}"));

                                    // сохранение файла
                                    bwr.Write(buf);
                                    bwr.Close();


                                    // вывод информации
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"[{DateTime.Now:t}] Файл отчета \"{fileName}\" успешно получен от клиента [{_client.Client.RemoteEndPoint}] и сохранен на сервере(папка App_Data).");
                                    Console.WriteLine($"Отчет содержит данные о работе отеля \"{hotelReport.HotelName}\" в период с [{hotelReport.PeriodStart}] до [{hotelReport.PeriodEnd}].");
                                    Console.ResetColor();

                                    // TODO: отправлять подтверждение клиенту
                                    break;
                                }

                                //TODO: другие команды

                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"[{DateTime.Now:t}] Сервер не поддерживает команду \"{command}\".");
                                Console.ResetColor();
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();

                        Stream?.Close();
                        _client?.Close();
                        return;
                    } // catch
                } // while
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}\n");
            }
        }
    }
}
