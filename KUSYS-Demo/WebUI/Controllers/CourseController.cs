using DataAccess.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; 
using Models.Concrete; 

namespace WebUI.Controllers
{
    public class CourseController : Controller
    {
        #region Members
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        /// <summary>
        /// Controller construcor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get dfault page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var courseList = _unitOfWork.CourseRepository.GetAll();
            return View(courseList);
        }

        #region Add

        /// <summary>
        /// Get default add view
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Add course to database
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(Course course)
        {
            // Check model is valid or not
            if (ModelState.IsValid)
            {
                // Add to database
                _unitOfWork.CourseRepository.Add(course);
                await _unitOfWork.SaveAsync();

                // Show message as success
                TempData["success"] = "Course Added Successfully";
                return RedirectToAction(nameof(Index));
            }

            // Show message as error
            TempData["error"] = "Process Failed";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit

        /// <summary>
        /// Get course edit view
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            // Get course by id
            var course = _unitOfWork.CourseRepository.GetFirstOrDefault(s => s.Id == id);
            return View(course);
        }

        /// <summary>
        /// Add course to database
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            // Get all course
            var courseList = _unitOfWork.CourseRepository.GetAll();

            // Find this course by id
            var temp = courseList.Where(x => x.Id == course.Id).FirstOrDefault();
            temp.Name = course.Name;
            temp.Code= course.Code;


            // Update course
            _unitOfWork.CourseRepository.Update(temp);
            await _unitOfWork.SaveAsync(); 

            // Show messages
            TempData["success"] = "Course Updated Successfully";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete

        /// <summary>
        /// Get course delete view
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            // Get course by id
            var course = _unitOfWork.CourseRepository.GetFirstOrDefault(s => s.Id == id);
            return View(course);
        }

        /// <summary>
        /// Delete course from database
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(Course course)
        {
            // Get all course
            var courseList = _unitOfWork.CourseRepository.GetAll();

            // Find course by id
            var temp = courseList.Where(x => x.Id == course.Id).FirstOrDefault();
            temp.Name = course.Name;
            temp.Code = course.Code;
            temp.Id= course.Id;

            // Delete item from database
            _unitOfWork.CourseRepository.Remove(temp);
            await _unitOfWork.SaveAsync(); 

            // Show message
            TempData["success"] = "Course Removed Successfully";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #endregion
    }
}
