using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.BLL.Interfaces
{
    public interface IDataService
    {
        IPersonsService Persons { get; }
        IWorkersService Workers { get; }
        IClientsService Clients { get; }
        IResidencesService Residences { get; }
        ISchedulesService Schedules { get; }
        IHotelRoomsService HotelRooms { get; }
        IHotelService Hotel { get; }

        int SaveChanges();
        int Initialize();
    }
}
