using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using HotelManager.BLL.Interfaces;
using HotelManager.BLL.Models;
using HotelManager.DAL.Interfaces;

namespace HotelManager.BLL.Services
{
    public class PersonsService : IPersonsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonsService(IUnitOfWork unityOfWork, IMapper mapper)
        {
            _unitOfWork = unityOfWork;
            _mapper = mapper;
        }

        public void Add(Person person)
        {
            _unitOfWork.Persons.Add(_mapper.Map<Person, DAL.Entities.Person>(person));
            _unitOfWork.SaveChanges();
        }

        public Person Find(int id)
        {
            var person = _unitOfWork.Persons.Find(id);

            return _mapper.Map<Person>(person);
        }

        public IEnumerable<Person> Get()
        {
            return _mapper.Map<IEnumerable<DAL.Entities.Person>, IEnumerable<Person>>(_unitOfWork.Persons.Get());
        }

        public void Remove(int id)
        {
            _unitOfWork.Persons.Remove(id);
            _unitOfWork.SaveChanges();
        }
    }
}
