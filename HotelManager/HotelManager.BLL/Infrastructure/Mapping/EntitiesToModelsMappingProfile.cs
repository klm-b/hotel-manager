using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelManager;

namespace HotelManager.BLL.Infrastructure.Mapping
{
    public class EntitiesToModelsMappingProfile : Profile
    {
        public EntitiesToModelsMappingProfile()
        {
            // TODO: при маппинге некторые объекты дублируются. Переделать
            CreateMap<DAL.Entities.HotelRoom, Models.HotelRoom>().ReverseMap();
            CreateMap<DAL.Entities.Person, Models.Person>().ReverseMap();
            CreateMap<DAL.Entities.Client, Models.Client>().ReverseMap();
            CreateMap<DAL.Entities.Worker, Models.Worker>().ReverseMap();
            CreateMap<DAL.Entities.WeeklySchedule, Models.WeeklySchedule>().ReverseMap();
            CreateMap<DAL.Entities.Residence, Models.Residence>().ReverseMap();
            CreateMap<DAL.Entities.Hotel, Models.Hotel>().ReverseMap();
        }
    }
}
