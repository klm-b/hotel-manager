using System.Linq;
using AutoMapper;
using HotelManager.BLL.Interfaces;
using HotelManager.BLL.Models;
using HotelManager.DAL.Interfaces;

namespace HotelManager.BLL.Services
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelService(IUnitOfWork unityOfWork, IMapper mapper)
        {
            _unitOfWork = unityOfWork;
            _mapper = mapper;
        }

        public Hotel Get()
        {
            var hotel = _unitOfWork.Hotels.Get().First();
            return _mapper.Map<DAL.Entities.Hotel, Hotel>(hotel);
        }

        public void Update(Hotel hotel)
        {
            var oldHotel = _unitOfWork.Hotels.Get().First();
            _mapper.Map(hotel, oldHotel);
            _unitOfWork.SaveChanges();
        }
    }
}
