using System.Collections.Generic;
using HotelManager.BLL.Interfaces;
using HotelManager.BLL.Models;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using HotelManager.DAL.Interfaces;

namespace HotelManager.BLL.Services
{
    public class HotelRoomsService : IHotelRoomsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelRoomsService(IUnitOfWork unityOfWork, IMapper mapper)
        {
            _unitOfWork = unityOfWork;
            _mapper = mapper;
        }

        public void Add(HotelRoom hotelRoom)
        {
            _unitOfWork.HotelRooms.Add(_mapper.Map<HotelRoom, DAL.Entities.HotelRoom>(hotelRoom));
            _unitOfWork.SaveChanges();
        }

        public HotelRoom Find(int id)
        {
            var room = _unitOfWork.HotelRooms.Find(id);

            return _mapper.Map<HotelRoom>(room);
        }

        public IEnumerable<HotelRoom> Get()
        {
            return _mapper.Map<IEnumerable<DAL.Entities.HotelRoom>, IEnumerable<HotelRoom>>(_unitOfWork.HotelRooms.Get());
        }

        public void Remove(int id)
        {
            _unitOfWork.Clients.Remove(id);
        }
    }
}
