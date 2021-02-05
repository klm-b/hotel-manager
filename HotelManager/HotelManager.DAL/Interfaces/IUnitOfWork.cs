using HotelManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Person> Persons { get; }
        IRepository<Client> Clients { get; }
        IRepository<Worker> Workers { get; }
        IRepository<Residence> Residences { get; }
        IRepository<WeeklySchedule> Schedules { get; }
        IRepository<HotelRoom> HotelRooms { get; }
        IRepository<Hotel> Hotels { get; }

        int SaveChanges();
        int Initialize();
    }
}
