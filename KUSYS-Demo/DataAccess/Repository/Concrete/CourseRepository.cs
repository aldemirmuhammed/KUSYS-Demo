
using DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Models.Concrete;

namespace DataAccess.Repository.Concrete
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        /// <summary>
        /// Create db context only read
        /// </summary>
        private readonly KUSYSDbContext _db;

        /// <summary>
        /// Constructor course repo
        /// </summary>
        /// <param name="db"></param>
        public CourseRepository(KUSYSDbContext db) : base(db)
        {
            _db = db;

            // Get course and anrollment and match both of them
            foreach (var item in _db.Courses)
            {
                foreach (var item2 in _db.Enrollments)
                {
                    if (item.Id == item2.CourseId)
                        item.Enrollments.Add(item2);
                }
            }
        }


        /// <summary>
        /// Update course
        /// </summary>
        /// <param name="course"></param>
        public void Update(Course course)
        {
            _db.Attach(course);
            _db.Entry(course).State = EntityState.Modified;
        }

        /// <summary>
        /// Get course by course id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Course GetCourseById(int Id)
        {
            return _db.Courses.Where(x => x.Id == Id).FirstOrDefault();
        }
    }
}
