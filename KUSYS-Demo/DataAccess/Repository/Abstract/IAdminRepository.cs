
using Models.Concrete;

namespace DataAccess.Repository.Abstract
{
    /// <summary>
    /// Admin repo interface
    /// </summary>
    public interface IAdminRepository : IRepository<Admin>
    {
        /// <summary>
        /// Update admin
        /// </summary>
        /// <param name="admin"></param>
        void Update(Admin admin);

        /// <summary>
        /// Get admin user bby id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Admin GetAdminUser(int id);


    }
}
