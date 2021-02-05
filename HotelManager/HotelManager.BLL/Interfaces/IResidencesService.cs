using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.BLL.Models;

namespace HotelManager.BLL.Interfaces
{
    public interface IResidencesService
    {
        void Add(Residence residence);
        IEnumerable<Residence> Get();
        void Remove(int id);
        Residence Find(int id);
    }
}
