
using DataAccess.Repository.Abstract;
using Models.Concrete;

namespace DataAccess.Repository.Concrete
{
    public class EnrollmentRepository : RepositoryBase<Enrollment>, IEnrollmentRepository
    {
        /// <summary>
        /// Create db context only read
        /// </summary>
        private readonly KUSYSDbContext _db;


        /// <summary>
        /// Constructor enrollment repo
        /// </summary>
        /// <param name="db"></param>
        public EnrollmentRepository(KUSYSDbContext db) : base(db)
        {
            _db = db;

            // Get student and anrollment and match both of them
            foreach (var item in _db.Enrollments)
            {
                foreach (var item2 in _db.Students)
                {
                    if (item.StudentId == item2.Id)
                        item.Student = item2;
                }
            }

            // Get course and anrollment and match both of them
            foreach (var item in _db.Enrollments)
            {
                foreach (var item2 in _db.Courses)
                {
                    if (item.CourseId == item2.Id)
                        item.Course = item2;
                }
            }
        }

        /// <summary>
        /// Get course by student id
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public IEnumerable<Course> GetCourseByUser(int studentId)
        {
            // Get student's enrollmensts as student is
            var enrollments = _db.Enrollments.Where(s => s.StudentId == studentId).ToList();
            var courseList = _db.Courses.ToList();

            // Get  courses that the student has not enrollment for
            foreach (var item in enrollments)
            {
                foreach (var item2 in _db.Courses.ToList())
                {
                    if (item.CourseId == item2.Id)
                        courseList.Remove(item2);
                }
            }

            return courseList;
        }

        /// <summary>
        /// Update enrollment
        /// </summary>
        /// <param name="enrollment"></param>
        public void Update(Enrollment enrollment)
        {
            _db.Update(enrollment);
        }
    }
}
