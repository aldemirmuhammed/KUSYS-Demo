
using Models.Concrete;

namespace DataAccess.Repository.Abstract
{
    /// <summary>
    /// Enrollment repo interface
    /// </summary>
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        /// <summary>
        /// Update enrollment
        /// </summary>
        /// <param name="Enrollment"></param>
        void Update(Enrollment Enrollment);

        /// <summary>
        /// Get course by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<Course> GetCourseByUser(int userId);

    }
}
