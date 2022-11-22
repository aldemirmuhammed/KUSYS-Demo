
using DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repository.Concrete
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Create db context only read
        /// </summary>
        private readonly KUSYSDbContext _db;
        internal DbSet<T> _dbSet;

        /// <summary>
        /// Constructor of repository base
        /// </summary>
        /// <param name="db"></param>
        public RepositoryBase(KUSYSDbContext db)
        {
            _db = db; 
            _dbSet = _db.Set<T>();
        }

        /// <summary>
        /// Add item to database
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Remove item from database
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Get all item incoming T type object
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
                query = query.Where(filter);
            query = AddIncludeOperationToQueryIfIncludePropertiesIsNotNull(query, includeProperties);

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filter);
            query = AddIncludeOperationToQueryIfIncludePropertiesIsNotNull(query, includeProperties);

            return query.FirstOrDefault();
        }

        static IQueryable<T> AddIncludeOperationToQueryIfIncludePropertiesIsNotNull(IQueryable<T> query, string? includeProperties)
        {
            if (includeProperties != null)
            {
                foreach (var prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(prop);
            }
            return query;
        }
    }
}
