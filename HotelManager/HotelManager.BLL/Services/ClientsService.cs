using System.Collections.Generic;
using HotelManager.BLL.Models;
using HotelManager.BLL.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using HotelManager.DAL.Interfaces;

namespace HotelManager.BLL.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientsService(IUnitOfWork unityOfWork, IMapper mapper)
        {
            _unitOfWork = unityOfWork;
            _mapper = mapper;
        }

        public void Add(Client client)
        {
            _unitOfWork.Clients.Add(_mapper.Map<Client, DAL.Entities.Client>(client));
            _unitOfWork.SaveChanges();
        }

        public Client Find(int id)
        {
            var client = _unitOfWork.Clients.Find(id);

            return _mapper.Map<Client>(client);
        }


        public IEnumerable<Client> Get()
        {
            return _mapper.Map<IEnumerable<DAL.Entities.Client>, IEnumerable<Client>>(_unitOfWork.Clients.Get());
        }

        public void Remove(int id)
        {
            _unitOfWork.Clients.Remove(id);
        }
    }
}
