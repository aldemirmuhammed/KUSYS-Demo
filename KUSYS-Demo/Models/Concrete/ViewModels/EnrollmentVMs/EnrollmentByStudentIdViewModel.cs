 
namespace Models.Concrete.ViewModels.EnrollmentVMs
{
    public class EnrollmentByStudentIdViewModel
    {
        public IEnumerable<Course> CourseList { get; set; }
        public IEnumerable<Enrollment> EnrollmentList { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }

    }
}
