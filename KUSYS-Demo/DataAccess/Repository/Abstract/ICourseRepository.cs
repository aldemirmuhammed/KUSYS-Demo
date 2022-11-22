
using Models.Concrete;

namespace DataAccess.Repository.Abstract
{
    /// <summary>
    /// Course repo interface
    /// </summary>
    public interface ICourseRepository : IRepository<Course>
    {
        /// <summary>
        /// Update course
        /// </summary>
        /// <param name="course"></param>
        void Update(Course course);

        /// <summary>
        /// Get course by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Course GetCourseById(int Id);
    }
}
