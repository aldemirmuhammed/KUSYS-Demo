using DataAccess.Repository.Abstract;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Concrete;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Models.Concrete.ViewModels;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        #region Members
        /// <summary>
        /// Create ıunitof work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor
        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Login
        /// <summary>
        /// return Login page as did not logining
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> login(UserLogin model)

        {
            // Check model is valid
            if (ModelState.IsValid)
            {
                // Get user from user repo as username and password
                var isUser= _unitOfWork.UserRepository.GetUserByUsernamePassword(model.UserName, model.Password);
                if (isUser != null)
                {
                    // Create Claims for authentication
                    List<Claim> userClaims = new List<Claim>();
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.UserName));
                    userClaims.Add(new Claim(ClaimTypes.Surname, isUser.UserName.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Role, isUser.Role.ToString()));

                  
                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Set remember properties
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
                }
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region Profile

        /// <summary>
        /// Get profile as current user
        /// </summary>
        /// <returns></returns>
        public IActionResult Profile()
        {
            // Get student profile
            var student = _unitOfWork.StudentRepository.GetFirstOrDefault(s => s.UserId == UserRolesViewModel.Instance.CurrentUser.ID);
            return View(student);
        }

        /// <summary>
        /// Update profile
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Profile(Student student)
        {
            // Get all user
            var list = _unitOfWork.UserRepository.GetAll();

            // Get all student
            var studenList = _unitOfWork.StudentRepository.GetAll();

            // Get user as student id
            var user = list.Where(x => x.ID == studenList.Where(x => x.Id == student.Id).FirstOrDefault().UserId).FirstOrDefault();
            user.UserName = student.UserName;
            user.Password = student.Password;

            // Get student by  id
            var temp = studenList.Where(x => x.Id == student.Id).FirstOrDefault();

            temp.UserId = user.ID;
            temp.Name = student.Name;
            temp.UserName = student.UserName;
            temp.LastName = student.LastName;
            temp.BirthDate = student.BirthDate;
            temp.Password = student.Password;
            temp.StudentNo = student.StudentNo;

            // Update student as temp
            _unitOfWork.StudentRepository.Update(temp);
            await _unitOfWork.SaveAsync();

            // Update user as user
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();

            TempData["success"] = "Student Updated Successfully";
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
