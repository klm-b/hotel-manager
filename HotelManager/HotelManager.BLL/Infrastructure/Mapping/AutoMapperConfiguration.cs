using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace HotelManager.BLL.Infrastructure.Mapping
{
    class AutoMapperConfiguration
    {
        public static IMapper Mapper { get; private set; }

        public static void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EntitiesToModelsMappingProfile>();
                cfg.AddProfile<ModelsToEntitiesMappingProfile>();
            });

            Mapper = config.CreateMapper();
        }
    }
}
