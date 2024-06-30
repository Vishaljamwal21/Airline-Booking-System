using System.Linq.Expressions;

namespace AirlineBookingSystem.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(int id);
        void RemoveRange(IEnumerable<T> values);
        T Get(int id);
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );
        T FirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );
    }
}
