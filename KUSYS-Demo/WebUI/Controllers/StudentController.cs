using DataAccess;
using DataAccess.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Concrete;
using Models.Helper;

namespace WebUI.Controllers
{
    public class StudentController : Controller
    {

        #region Members
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get all students to index view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var studentList = _unitOfWork.StudentRepository.GetAll();
            return View(studentList);
        }

        #region SeeDetail

        /// <summary>
        /// See detail to student in admin view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult SeeDetail(int id)
        {
            // Get student by id
            var studentDet = _unitOfWork.StudentRepository.GetFirstOrDefault(s => s.Id == id);
            return View(studentDet);
        }

        #endregion

        #region Add
        /// <summary>
        /// Get add view
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Add student to database
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(Student student)
        {
            // Create user for adding to student
            Models.Concrete.User user = new User()
            {
                UserName = student.UserName,
                Password = student.Password,
                Role = (int)HelperEnum.RoleType.Student,
                CreateDate = DateTime.Now
            };

            // Set student user object as user
            student.User = user;

            // Add to database 
            _unitOfWork.StudentRepository.Add(student);
            await _unitOfWork.SaveAsync();

            // Show message
            TempData["success"] = "Student Added Successfully";
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Edit

        /// <summary>
        /// Edit student view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            // Get student by id
            var student = _unitOfWork.StudentRepository.GetFirstOrDefault(s => s.Id == id);
            return View(student);
        }

        /// <summary>
        /// Edit and save to database
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            // Get all user and student
            var list = _unitOfWork.UserRepository.GetAll();
            var studenList =_unitOfWork.StudentRepository.GetAll();

            // Select user in student list by id
            var user = list.Where(x => x.ID == studenList.Where(x => x.Id == student.Id).FirstOrDefault().UserId).FirstOrDefault();
            user.UserName = student.UserName;
            user.Password = student.Password;

            // Set student as temp by id
            var temp = studenList.Where(x => x.Id == student.Id).FirstOrDefault();

            // Set temp
            temp.UserId = user.ID;
            temp.Name = student.Name;
            temp.UserName = student.UserName;
            temp.LastName = student.LastName;
            temp.BirthDate = student.BirthDate;
            temp.Password = student.Password;
            temp.StudentNo = student.StudentNo;
            
            // Update student
            _unitOfWork.StudentRepository.Update(temp);
            await _unitOfWork.SaveAsync();

            // Update user
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();

            // Show message
            TempData["success"] = "Student Updated Successfully";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete

        /// <summary>
        /// Get student for delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            // Get student by id
            var student = _unitOfWork.StudentRepository.GetFirstOrDefault(s => s.Id == id);
            return View(student);
        }

        /// <summary>
        /// Delete student from database
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(Student student)
        {
            // Get all user and student
            var list = _unitOfWork.UserRepository.GetAll();
            var studenList = _unitOfWork.StudentRepository.GetAll();

            var tempStudent = studenList.Where(x => x.Id == student.Id).FirstOrDefault();
            var user = list.Where(x => x.ID == tempStudent.UserId).FirstOrDefault();

            // Delete student and save 
            _unitOfWork.StudentRepository.Remove(tempStudent);
            await _unitOfWork.SaveAsync();

            // Delete user and save 
            _unitOfWork.UserRepository.Remove(user);
            await _unitOfWork.SaveAsync();

            TempData["success"] = "Student Removed Successfully";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #endregion
    }
}
