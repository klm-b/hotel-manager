using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using HotelManager.BLL.Interfaces;
using HotelManager.BLL.Models;
using HotelManager.DAL.Interfaces;

namespace HotelManager.BLL.Services
{
    public class ResidencesService : IResidencesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ResidencesService(IUnitOfWork unityOfWork, IMapper mapper)
        {
            _unitOfWork = unityOfWork;
            _mapper = mapper;
        }

        public void Add(Residence residence)
        {
            _unitOfWork.Residences.Add(_mapper.Map<Residence, DAL.Entities.Residence>(residence));
            _unitOfWork.SaveChanges();
        }

        public Residence Find(int id)
        {
            var residence = _unitOfWork.Residences.Find(id);

            return _mapper.Map<Residence>(residence);
        }

        public IEnumerable<Residence> Get()
        {
            return _mapper.Map<IEnumerable<DAL.Entities.Residence>, IEnumerable<Residence>>(_unitOfWork.Residences.Get());
        }

        public void Remove(int id)
        {
            _unitOfWork.Residences.Remove(id);
            _unitOfWork.SaveChanges();
        }

    }
}
