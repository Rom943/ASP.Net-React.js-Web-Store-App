using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShopApi.Context;
using ShopApi.Repositories.IRepositories;
using System.Linq.Expressions;


namespace ShopApi.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
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

        public IQueryable<T> FindAll()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition)
        {
            return _context.Set<T>().Where(condition);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public T Update(T item)
        {
            EntityEntry<T> updatedItem = _context.Set<T>().Update(item);
            Save();
            return updatedItem.Entity;
        }

        public async Task<List<T>> GetOrderByDescending(Expression<Func<T, double>> propertySelector)
        {
            return await _context.Set<T>()
                .OrderByDescending(propertySelector)
                .ToListAsync();
        }

        public async Task <List<T>> FindEntityWithMaxRelatedChildrenAsync<TChild>(
            Expression<Func<T, ICollection<TChild>>> childCollectionSelector) where TChild : class
        {
            var entities = await _context.Set<T>()
                .Include(childCollectionSelector)
                .ToListAsync();

            var entityWithMaxRelatedChildren = entities
                .OrderByDescending(entity => childCollectionSelector.Compile()(entity).Count)
                .ToList();

            return entityWithMaxRelatedChildren;
        }

        public Task<List<T>> GetOrderByDescendingDouble(Expression<Func<T, double>> propertySelector)
        {
            throw new NotImplementedException();
        }
    }
}
