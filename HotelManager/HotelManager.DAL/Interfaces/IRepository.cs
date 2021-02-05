using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HotelManager.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        void Add(TEntity item);
        IEnumerable<TEntity> Get();
        void Remove(int id);
        TEntity Find(int id);
    }
}
