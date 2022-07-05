using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SystemInventory.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Delete(int id);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        void Update(T entity);
        void Insert(T entity);

        /// <summary>
        /// Get all without filter
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Get();

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// Get all with filter
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string properties = null
            );

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> filter = null,
            string properties = null
            );
    }
}
