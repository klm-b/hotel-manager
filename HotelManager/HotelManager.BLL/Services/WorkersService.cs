using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using HotelManager.BLL.Interfaces;
using HotelManager.BLL.Models;
using HotelManager.DAL.Interfaces;

namespace HotelManager.BLL.Services
{
    public class WorkersService : IWorkersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkersService(IUnitOfWork unityOfWork, IMapper mapper)
        {
            _unitOfWork = unityOfWork;
            _mapper = mapper;
        }

        public void Add(Worker worker)
        {
            _unitOfWork.Persons.Add(_mapper.Map<Worker, DAL.Entities.Worker>(worker));
            _unitOfWork.SaveChanges();
        }

        public Worker Find(int id)
        {
            var worker = _unitOfWork.Workers.Find(id);

            return _mapper.Map<Worker>(worker);
        }

        public IEnumerable<Worker> Get()
        {
            return _mapper.Map<IEnumerable<DAL.Entities.Worker>, IEnumerable<Worker>>(_unitOfWork.Workers.Get());
        }

        public void Remove(int id)
        {
            _unitOfWork.Workers.Remove(id);
            _unitOfWork.SaveChanges();
        }
    }
}
