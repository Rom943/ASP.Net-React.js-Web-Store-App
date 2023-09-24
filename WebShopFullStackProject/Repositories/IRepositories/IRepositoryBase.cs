using System.Linq.Expressions;

namespace ShopApi.Repositories.IRepositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition);
        Task<T> Create(T item);
        T Update(T item);
        void Delete(T item);
        void Save();


    }
}
