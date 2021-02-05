using HotelManager.DAL.Entities;
using HotelManager.DAL.Interfaces;
using HotelManager.DAL.Repositories;

namespace HotelManager.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseContext _db;

        public EFUnitOfWork(IDatabaseContext databaseContext)
        {
            _db = databaseContext;
        }


        public int SaveChanges()
        {
            return _db.SaveChanges();
        }


        public void Dispose()
        {
            _db.Dispose();
        }

        public int Initialize()
        {

            return _db.Initialize();
        }


        // репозитории
        private IRepository<Person> _persons;
        private IRepository<Client> _clients;
        private IRepository<Worker> _workers;
        private IRepository<Residence> _residences;
        private IRepository<WeeklySchedule> _schedules;
        private IRepository<HotelRoom> _hotelRooms;
        private IRepository<Hotel> _hotels;



        public IRepository<Person> Persons =>
            _persons ?? (_persons = new EFRepository<Person>(_db));

        public IRepository<Client> Clients =>
            _clients ?? (_clients = new EFRepository<Client>(_db));

        public IRepository<Worker> Workers =>
            _workers ?? (_workers = new EFRepository<Worker>(_db));

        public IRepository<Residence> Residences =>
            _residences ?? (_residences = new EFRepository<Residence>(_db));

        public IRepository<WeeklySchedule> Schedules =>
            _schedules ?? (_schedules = new EFRepository<WeeklySchedule>(_db));

        public IRepository<HotelRoom> HotelRooms =>
            _hotelRooms ?? (_hotelRooms = new EFRepository<HotelRoom>(_db));

        public IRepository<Hotel> Hotels =>
             _hotels ?? (_hotels = new EFRepository<Hotel>(_db));
    }
}
