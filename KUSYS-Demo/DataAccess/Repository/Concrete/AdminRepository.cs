
using DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Models.Concrete;

namespace DataAccess.Repository.Concrete
{
    public class AdminRepository : RepositoryBase<Admin>, IAdminRepository
    {
        /// <summary>
        /// Create db context only read
        /// </summary>
        private readonly KUSYSDbContext _db;

        /// <summary>
        /// Constructor admin repo
        /// </summary>
        /// <param name="db"></param>
        public AdminRepository(KUSYSDbContext db) : base(db)
        {
            _db = db;

            // Get user and admin and match bot of them
            foreach (var item in _db.Admins)
            {
                foreach (var item2 in _db.Users)
                {
                    if (item.UserId == item2.ID)
                        item.User = item2;
                }
            }

        }

        /// <summary>
        /// Update admin
        /// </summary>
        /// <param name="admin"></param>
        public void Update(Admin admin)
        {
            _db.Update(admin);
        }

        /// <summary>
        /// Get admin by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Admin GetAdminUser(int id)
        {
            var user = _db.Admins.FirstOrDefault(s => s.ID == id);
            return user;
        }


    }
}
