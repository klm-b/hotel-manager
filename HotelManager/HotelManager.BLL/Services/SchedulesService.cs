using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using HotelManager.BLL.Interfaces;
using HotelManager.BLL.Models;
using HotelManager.DAL.Interfaces;

namespace HotelManager.BLL.Services
{
    public class SchedulesService : ISchedulesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SchedulesService(IUnitOfWork unityOfWork, IMapper mapper)
        {
            _unitOfWork = unityOfWork;
            _mapper = mapper;
        }

        public void Add(WeeklySchedule schedule)
        {
            _unitOfWork.Schedules.Add(_mapper.Map<WeeklySchedule, DAL.Entities.WeeklySchedule>(schedule));
            _unitOfWork.SaveChanges();
        }

        public WeeklySchedule Find(int id)
        {
            var schedule = _unitOfWork.Schedules.Find(id);

            return _mapper.Map<WeeklySchedule>(schedule);
        }

        public IEnumerable<WeeklySchedule> Get()
        {
            return _mapper.Map<IEnumerable<DAL.Entities.WeeklySchedule>, IEnumerable<WeeklySchedule>>(_unitOfWork.Schedules.Get());
        }

        public void Remove(int id)
        {
            _unitOfWork.Schedules.Remove(id);
            _unitOfWork.SaveChanges();
        }
    }
}
