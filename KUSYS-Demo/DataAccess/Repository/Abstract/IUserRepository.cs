
using Models.Concrete;

namespace DataAccess.Repository.Abstract
{
    /// <summary>
    /// User repo interface
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="User"></param>
        void Update(User User);


        /// <summary>
        /// Get user as username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User? GetUserByUsernamePassword(string username,string password);

    }
}
