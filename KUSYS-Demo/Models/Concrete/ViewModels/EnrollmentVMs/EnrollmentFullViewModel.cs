 
namespace Models.Concrete.ViewModels.EnrollmentVMs
{
    public class EnrollmentFullViewModel
    {
        public IEnumerable<Enrollment> EnrollmentList { get; set; }
        public IEnumerable<Student> StudentList { get; set; }
        public IEnumerable<Course> CourseList { get; set; }
    }
}
