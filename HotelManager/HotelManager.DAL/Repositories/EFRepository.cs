using HotelManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.DAL.Repositories
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly IDatabaseContext _context;
        private readonly IDbSet<TEntity> _dbSet;

        public EFRepository(IDatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity item) => _dbSet.Add(item);

        public bool Exists(int id) => _dbSet.Find(id) != null;

        public TEntity Find(int id) => _dbSet.Find(id);

        public IEnumerable<TEntity> Get() => _dbSet.ToList();

        public void Remove(int id) => _dbSet.Remove(_dbSet.Find(id));
    }
}
