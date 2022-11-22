
using Models.Concrete;

namespace DataAccess.Repository.Abstract
{
    /// <summary>
    /// Student repo interface
    /// </summary>
    public interface IStudentRepository : IRepository<Student>
    {
        /// <summary>
        /// Update student
        /// </summary>
        /// <param name="student"></param>
        void Update(Student student);

        /// <summary>
        /// Get students course list as student id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Course> GetStudentCourseList(int id);

        /// <summary>
        /// Get student as user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student GetStudentUser(int id);
    }
}
