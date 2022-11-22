
using DataAccess.Repository.Abstract;
using Models.Concrete;
using Models.Concrete.ViewModels;

namespace DataAccess.Repository.Concrete
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        /// <summary>
        /// Create db context only read
        /// </summary>
        private readonly KUSYSDbContext _db;

        /// <summary>
        /// Constructor of user repo
        /// </summary>
        /// <param name="db"></param>
        public UserRepository(KUSYSDbContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// Get user  as username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User? GetUserByUsernamePassword(string username, string password)
        {
            var user = _db.Users.FirstOrDefault(s => s.UserName == username && s.Password == password);
            if(user == null)
                return null;

            // Set current user
            UserRolesViewModel.Instance.CurrentUser = user;
            return user;
        }
      
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user"></param>
        public void Update(User user)
        {
            _db.Update(user);
        }
    }
}
