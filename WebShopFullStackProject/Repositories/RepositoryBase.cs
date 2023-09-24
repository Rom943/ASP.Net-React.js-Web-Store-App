using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShopApi.Context;
using ShopApi.Repositories.IRepositories;
using System.Linq.Expressions;


namespace ShopApi.Repositories
{
    public class RepositoryBase<T>:IRepositoryBase<T>where T : class
    {
        private readonly MainContext _context;

        public RepositoryBase(MainContext _context)
        {
            this._context = _context ?? throw new ArgumentNullException(nameof(_context));
        }

        public async Task<T> Create(T item)
        {
            EntityEntry<T> newItem = await _context.Set<T>().AddAsync(item);
            Save();
            return newItem.Entity;
        }

        public void Delete(T item)
        {
             _context.Set<T>().Remove(item);
            Save();
        }

        public   IQueryable<T> FindAll()
        {
            return  _context.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition)
        {
            return _context.Set<T>().Where(condition);
        }

        public  void Save()
        {
            _context.SaveChanges();
        }

        public T Update(T item)
        {
            EntityEntry<T> updatedItem = _context.Set<T>().Update(item);
            Save();
            return updatedItem.Entity;
        }
    }

}
