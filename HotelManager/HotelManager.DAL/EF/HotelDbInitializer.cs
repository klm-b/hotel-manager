using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.DAL.Entities;

namespace HotelManager.DAL.EF
{
    class HotelDbInitializer : DropCreateDatabaseIfModelChanges<HotelDbContext>
    {
        protected override void Seed(HotelDbContext db)
        {

           var hotel = new Hotel()
           {
               Name = "Emirates Palace",
               Address = "Address",
               FloorCount = 10
           };

            var rooms = new List<HotelRoom>();
            for (int i = 0; i < 10; i++)
            {
                rooms.Add(GenerateHotelRoom(hotel));
            }

            var workers = new List<Worker>();
            for (int i = 0; i < 10; i++)
            {
                workers.Add(GenerateWorker(hotel));
            }


            var clients = new List<Client>();
            for (int i = 0; i < 20; i++)
            {
                clients.Add(GenerateClient());
            }


            var residences = new List<Residence>();
            for (int i = 0; i < 20; i++)
            {
                residences.Add(GenerateResidence(rooms[GetRand(0, rooms.Count - 1)], clients[i]));
            }

            db.Workers.AddRange(workers);
            db.Clients.AddRange(clients);
            db.Residences.AddRange(residences);


            db.SaveChanges();

            base.Seed(db);
        }

        private static Client GenerateClient()
        {
            return new Client()
            {
                Surname = GenerateSurname(),
                Name = GenerateName(),
                Patronymic = GeneratePatronymic(),
                PassportNumber = GeneratePassportID(),
                Email = GenerateEmail(),
                PhoneNumber = GeneratePhoneNumber("38", "066"),
                City = GenerateCity(),
            };
        }

        public static Residence GenerateResidence(HotelRoom room, Client client)
        {
            return new Residence()
            {
                Client = client,
                HotelRoom = room,
                CheckInDate = DateTime.Today.Subtract(TimeSpan.FromDays(GetRand(5, 50))),
                CheckOutDate = DateTime.Today.AddDays(GetRand(1, 10))
            };
        }

        public static Worker GenerateWorker(Hotel hotel)
        {
            return new Worker()
            {
                Hotel = hotel,
                Name =  GenerateName(),
                Surname =  GenerateSurname(),
                PassportNumber =  GeneratePassportID(),
                Patronymic =  GeneratePatronymic(),
                Email =  GenerateEmail(),
                PhoneNumber =  GeneratePhoneNumber(),
                Working = true,
                WorkdaySalary = (decimal) GetRand(1000d, 10000),
                CleaningSchedule = new WeeklySchedule
                {
                    Monday =  GetRandOrNull(1, 7),
                    Tuesday =  GetRandOrNull(1, 7),
                    Wednesday =  GetRandOrNull(1, 7),
                    Thursday =  GetRandOrNull(1, 7),
                    Friday =  GetRandOrNull(1, 7),
                    Saturday =  GetRandOrNull(1, 7),
                    Sunday =  GetRandOrNull(1, 7),
                }
            };
        }

        public static HotelRoom GenerateHotelRoom(Hotel hotel)
        {
            return new HotelRoom()
            {
                Floor =  GetRand(1, hotel.FloorCount),
                PhoneNumber =  GeneratePhoneNumber(),
                Price =  GetRand(1000, 15000),
                RoomType =  GetRand(1, 10),
                Hotel = hotel
            };
        }


        public static readonly Random Random = new Random(); // объект для генерации случайных чисел

        public static int GetRand(int lo, int hi) => Random.Next(lo, hi + 1); // генерация int
        public static int GetRand(int hi) => Random.Next(hi + 1); // генерация int

        public static int? GetRandOrNull(int lo, int hi) => GetRand(1, 4) == 1 ? null : (int?)GetRand(lo, hi); // генерация int с возможным null

        public static double GetRand(double lo, double hi)
        {
            double result = lo + (hi - lo) * Random.NextDouble();
            return Math.Abs(result) < 0.7 ? 0 : result; // для генерации нулей
        } // генерация double

        // сгенерировать фамилию
        public static string GenerateSurname()
        {
            string[] surnames = {
                "Андрусейко", "Гущин", "Корнейчук", "Князев", "Кононов",
                "Кабанов", "Лапин", "Кондратьев", "Кудрявцев", "Пахомов",
                "Палий", "Щукин", "Овчинников", "Мамонтов", "Кузьмин",
                "Смирнов", "Иванов", "Кузнецов", "Соколов", "Попов", "Лебедев",
                "Козлов", "Новиков", "Морозов", "Петров", "Волков", "Соловьёв",
                "Васильев", "Зайцев", "Павлов", "Семёнов", "Голубев", "Виноградов",
                "Богданов", "Воробьёв", "Фёдоров", "Михайлов", "Беляев", "Тарасов", "Белов"
            };

            return surnames[GetRand(0, surnames.Length - 1)];
        }

        // сгенерировать имя
        public static string GenerateName()
        {
            // имена, фамилии и отчества для генератора ФИО
            string[] names = {
                "Жерар", "Никодим", "Казбек", "Осип", "Назар",
                "Спартак", "Донат", "Харитон", "Лука", "Гавриил",
                "Елисей", "Жигер", "Милан", "Геннадий", "Яромир",
                "Алан", "Александр", "Алексей", "Альберт", "Анатолий",
                "Андрей", "Антон", "Арсен", "Арсений", "Артем", "Артемий", "Артур", "Богдан", "Борис", "Вадим",
                "Валентин", "Валерий", "Василий", "Виктор", "Виталий", "Владимир", "Владислав", "Всеволод", "Вячеслав",
                "Геннадий", "Георгий", "Герман", "Глеб", "Гордей", "Григорий", "Давид", "Дамир", "Даниил", "Демид",
                "Демьян", "Денис", "Дмитрий", "Евгений", "Егор", "Елисей", "Захар", "Иван", "Игнат", "Игорь", "Илья",
                "Ильяс", "Камиль", "Карим", "Кирилл", "Клим", "Константин", "Лев", "Леонид", "Макар", "Максим", "Марат",
                "Марк", "Марсель", "Матвей", "Мирон", "Мирослав", "Михаил", "Назар", "Никита", "Николай", "Олег",
                "Павел", "Петр", "Платон", "Прохор", "Рамиль", "Ратмир", "Ринат", "Роберт", "Родион", "Роман",
                "Ростислав", "Руслан", "Рустам", "Савва", "Савелий", "Святослав", "Семен", "Сергей", "Станислав",
                "Степан", "Тамерлан", "Тимофей", "Тимур", "Тихон", "Федор", "Филипп", "Шамиль", "Эдуард", "Эльдар",
                "Эмиль", "Эрик", "Юрий", "Ян", "Ярослав"
            };

            return names[GetRand(0, names.Length - 1)];
        }

        // генерация емейла
        public static string GenerateEmail()
        {
            // массив эмейлов для генерации
            string[] emails =
            {
                "fiyipo2661@gmail.com", "sofyec@gmail.com", "ominousis@gmail.com", "urimp@gmail.com", "orerenge@gmail.com",
                "horounton@gmail.com", "owila@gmail.com", "astaler@gmail.com", "kufive@gmail.com", "ontoro@gmail.com",
                "rathenon@gmail.com", "lillilime@gmail.com", "aywemay@gmail.com", "gusonga@gmail.com", "feyli@gmail.com",
                "agaisugus@gmail.com", "irindort@gmail.com", "gofte@gmail.com", "acimmoflu@gmail.com", "rinesho@gmail.com"
            };

            return emails[GetRand(emails.Length - 1)];
        }

        // сгенерировать город
        public static string GenerateCity()
        {
            // города
            string[] cities = {
                "Донецк", "Москва", "Киев", "Сочи", "Харьков",
                "Львов", "Лондон", "Анапа", "Томск", "Липецк",
                "Глазов", "Владимир", "Курган", "Шахты", "Котлас"
            };

            return cities[GetRand(0, cities.Length - 1)];
        }

        // сгенерировать отчество
        public static string GeneratePatronymic()
        {
            string[] patronymics = {
                "Владимирович", "Фёдорович", "Максимович", "Богданович", "Васильевич",
                "Борисович", "Максимович", "Станиславович", "Романович", "Петрович",
                "Алексеевич", "Григорьевич", "Данилович", "Брониславович", "Юхимович"
            };

            return patronymics[GetRand(0, patronymics.Length - 1)];
        }

        // сгенерировать ФИО
        public static string GenerateSNP() => $"{GenerateSurname()} {GenerateName()} {GeneratePatronymic()}";
        public static string GenerateSurnameNP() => $"{GenerateSurname()} {GenerateName().First()}. {GeneratePatronymic().First()}.";

        // сгенерировать согласную букву
        public static char GenerateConsonant()
        {
            // согласные
            char[] consonants = {
                'б', 'в', 'г', 'д', 'ж',
                'з', 'й', 'к', 'л', 'м',
                'н', 'п', 'р', 'с', 'т',
                'ф', 'х', 'ц', 'ч', 'ш', 'щ'
            };

            return consonants[GetRand(0, consonants.Length - 1)];
        }

        // генерация bool
        public static bool GenerateBool() => GetRand(0, 1) == 0;


        // сгенерировать номер машины
        public static string GenerateNumber()
        {
            // буквы для номеров
            // используются те, которые присутствуют и в кириллице, и в латинице
            char[] numberLetters = "АВЕКМНОРСТУХ".ToCharArray();

            StringBuilder builder = new StringBuilder(8);
            for (int i = 0; i < 5; i++)
                builder.Append(
                    i == 2
                    ? $"{GetRand(1000, 9999)}"
                    : $"{numberLetters[GetRand(numberLetters.Length - 1)]}");

            return builder.ToString();
        }

        public static string GeneratePassportID()
        {
            // используются те, которые присутствуют и в кириллице, и в латинице
            char[] letters = "АВЕКМНОРСТУХ".ToCharArray();

            StringBuilder builder = new StringBuilder(8);
            builder.Append(letters[GetRand(letters.Length - 1)]);
            builder.Append(letters[GetRand(letters.Length - 1)]);
            builder.Append('-');
            builder.Append(GetRand(100000, 999999));

            return builder.ToString();
        }

        // сгенерировать номер телефона
        public static string GeneratePhoneNumber(string countryCode, string mobileOperator) => $"+{countryCode}{mobileOperator}{GetRand(1000000, 9999999)}";
        public static string GeneratePhoneNumber() => $"+{GetRand(11, 99)}{GetRand(100, 999)}{GetRand(1000000, 9999999)}";
    }
}
