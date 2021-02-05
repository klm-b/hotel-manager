using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelManager.BLL.Infrastructure.Mapping;
using HotelManager.BLL.Interfaces;
using HotelManager.DAL.Interfaces;

namespace HotelManager.BLL.Services
{
    public class DataService : IDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            AutoMapperConfiguration.Configure();
            _mapper = AutoMapperConfiguration.Mapper;
        }

        private IPersonsService _persons;
        private IWorkersService _workers;
        private IClientsService _clients;
        private IResidencesService _residences;
        private ISchedulesService _schedules;
        private IHotelRoomsService _hotelRooms;
        private IHotelService _hotel;

        public IPersonsService Persons => _persons ?? (_persons = new PersonsService(_unitOfWork, _mapper));
        public IWorkersService Workers => _workers ?? (_workers = new WorkersService(_unitOfWork, _mapper));
        public IClientsService Clients => _clients ?? (_clients = new ClientsService(_unitOfWork, _mapper));
        public IResidencesService Residences => _residences ?? (_residences = new ResidencesService(_unitOfWork, _mapper));
        public ISchedulesService Schedules => _schedules ?? (_schedules = new SchedulesService(_unitOfWork, _mapper));
        public IHotelRoomsService HotelRooms => _hotelRooms ?? (_hotelRooms = new HotelRoomsService(_unitOfWork, _mapper));
        public IHotelService Hotel => _hotel ?? (_hotel = new HotelService(_unitOfWork, _mapper));

        public int SaveChanges() => _unitOfWork.SaveChanges();

        public int Initialize() => _unitOfWork.Initialize();
    }
}

