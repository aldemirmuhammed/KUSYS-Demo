
using DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Models.Concrete;

namespace DataAccess.Repository.Concrete
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {

        /// <summary>
        /// Create db context only read
        /// </summary>
        private readonly KUSYSDbContext _db;



        /// <summary>
        /// Constructor of student repo
        /// </summary>
        public StudentRepository(KUSYSDbContext db) : base(db)
        {
            _db = db;

            // Get student and user and match both of them
            foreach (var item in _db.Students)
            {
                foreach (var item2 in _db.Users)
                {
                    if (item.UserId == item2.ID)
                        item.User = item2;
                }
            }

            // Get student and enrollments and match both of them
            foreach (var item in _db.Students)
            {
                foreach (var item2 in _db.Enrollments)
                {
                    if (item.Id == item2.StudentId)
                        item.Enrollments.Add(item2);
                }
            }
        }

        /// <summary>
        /// Update student
        /// </summary>
        /// <param name="student"></param>
        public void Update(Student student)
        {
            _db.Attach(student);
            _db.Entry(student).State = EntityState.Modified;
        }

        /// <summary>
        /// Get course list as student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Course> GetStudentCourseList(int id)
        {
            var student = _db.Students.FirstOrDefault(s => s.Id == id);

            List<Course> result = new List<Course>();

            foreach (var item in _db.Courses)
            {
                foreach (var item2 in student.Enrollments)
                {
                    if (item2.CourseId == item.Id)
                        result.Add(item);
                }
            }

            return result;

        }

        /// <summary>
        /// Get student by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Student GetStudentUser(int id)
        {
            var user = _db.Students.FirstOrDefault(s => s.Id == id);
            return user;
        }

    }
}
