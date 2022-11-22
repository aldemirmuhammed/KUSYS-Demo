using DataAccess.Repository.Abstract;
using DataAccess.Repository.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Concrete;
using Models.Concrete.ViewModels;
using Models.Concrete.ViewModels.EnrollmentVMs;

namespace WebUI.Controllers
{
    public class EnrollmentController : Controller
    {
        #region Members
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public EnrollmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get all enrollments for admin page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            // Create object and bind to view
            EnrollmentFullViewModel model = new()
            {
                EnrollmentList = _unitOfWork.EnrollmentRepository.GetAll().OrderBy(x => x.StudentId),
                StudentList = _unitOfWork.StudentRepository.GetAll(),
                CourseList = _unitOfWork.CourseRepository.GetAll(),
            };
            return View(model);
        }

        /// <summary>
        /// Get enrollments by student id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult GetListByStudentId(int id)
        {
            // Create object and bind to view
            EnrollmentByStudentIdViewModel model = new()
            {
                EnrollmentList = _unitOfWork.EnrollmentRepository.GetAll(eg => eg.StudentId == id),
                CourseList = _unitOfWork.StudentRepository.GetStudentCourseList(id),
                Student = _unitOfWork.StudentRepository.GetFirstOrDefault(s => s.Id == id)
            };
            return View(model);
        }

        /// <summary>
        /// Get students view for admin page
        /// </summary>
        /// <returns></returns>
        public IActionResult GetStudents()
        {
            // Get all students
            var studentList = _unitOfWork.StudentRepository.GetAll();
            return View(studentList);
        }

        /// <summary>
        /// Get course selected student id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult GetCourses(int id)
        {

            // Get non-student courses by user id
            var userEnrollmentList = _unitOfWork.EnrollmentRepository.GetCourseByUser(id);

            // Create model, set student and student's havent course list
            EnrollmentByStudentIdViewModel byStudentIdViewModel = new EnrollmentByStudentIdViewModel()
            {
                Student = _unitOfWork.StudentRepository.GetStudentUser(id),
                CourseList = userEnrollmentList

            };
            
            // Set student enrolled by admin
            UserRolesViewModel.Instance.EnrollmentUserByAdmin = byStudentIdViewModel.Student;
            return View(byStudentIdViewModel);
        }

        /// <summary>
        /// Get student enrollment view for student
        /// </summary>
        /// <returns></returns>
        public IActionResult MyEnrollments()
        {
            // Get enrollments as student id
            var _myEnrollment = _unitOfWork.StudentRepository.GetStudentUser(UserRolesViewModel.Instance.CurrentUser.Students.ElementAt(0).Id);
            return View(_myEnrollment);
        }

        /// <summary>
        /// Get addenrolment view 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult AddEnrollment(int id)
        {
            var _course = _unitOfWork.CourseRepository.GetCourseById(id);
            return View(_course);
        }

        /// <summary>
        /// Add enrollment to database for student
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddEnrollment(Course course)
        {
            // Get course by id
            var _course = _unitOfWork.CourseRepository.GetCourseById(course.Id);

            // Check model is valid
            if (ModelState.IsValid)
            {
                // Create model for adding to database
                Enrollment enrollment = new Enrollment()
                {
                    CourseId = course.Id,
                    StudentId = UserRolesViewModel.Instance.EnrollmentUserByAdmin.Id,
                };

                // Add to database
                _unitOfWork.EnrollmentRepository.Add(enrollment);
                await _unitOfWork.SaveAsync();

                // Check role type, If role is student redirect to student's myenrollment page, else redirec to ındex page in admin
                if (UserRolesViewModel.Instance.EnrollmentUserByAdmin.User.Role == UserRolesViewModel.Instance.CurrentUser.Role)
                {
                    TempData["success"] = "Student Added Successfully";
                    return RedirectToAction(nameof(MyEnrollments));
                }
                TempData["success"] = "Student Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Process Failed";
            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}
