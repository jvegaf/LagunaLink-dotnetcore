
namespace LagunaLink.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using LagunaLink.Web.Helpers;
    using LagunaLink.Web.Models;
    using System.Linq;
    using LagunaLink.Web.Data.Entities;
    using Microsoft.AspNetCore.Identity;
    using LagunaLink.Web.Data.Managers;
    using System;
    using Microsoft.AspNetCore.Http;

    public class AccountController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IMailHelper mailHelper;
        private readonly IStudentManager studentManager;

        public AccountController(IUserHelper userHelper, IMailHelper mailHelper, IStudentManager studentManager)
        {
            this.userHelper = userHelper;
            this.mailHelper = mailHelper;
            this.studentManager = studentManager;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to login.");
            return this.View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await this.userHelper.LogoutAsync();
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.Username);
                if (user == null)
                {
                    user = new User
                    {
                        Email = model.Username,
                        UserName = model.Username,
                        Registered = false,
                        LagunaRole = model.LagunaRole
                    };

                    var result = await this.userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }

                    var myToken = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
                    var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    this.mailHelper.SendMail(model.Username, "LagunaLink Email confirmation", $"<h1>LagunaLink Email Confirmation</h1>" +
                        $"To allow the user, " +
                        $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                    this.ViewBag.Message = "The instructions to allow your user has been sent to email.";
                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "The username is already registered.");
            }

            return this.View(model);
        }

        public IActionResult RegisterStudent()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterStudent(RegisterNewStudentViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                string userId = HttpContext.Session.GetString("userId");

                var student = this.studentManager.GetStudentByUserId(userId);
                if (student == null)
                {

                    student = new Student
                    {
                        UserId = userId,
                        Name = model.Name,
                        FirstSurname = model.FirstSurname,
                        LastSurname = model.LastSurname,
                        Phone = model.Phone
                    };

                    this.studentManager.AddStudent(student);
                    var result = await this.studentManager.SaveAllAsync();
                    
                    if (result != true)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }

                    var user = await this.userHelper.GetUserByIdAsync(userId);
                    user.Registered = true;
                    var _result = await this.userHelper.UpdateUserAsync(user);
                    
                    return RedirectToAction("Login", "Account");
                }

                this.ModelState.AddModelError(string.Empty, "The student is already registered.");
            }
            return this.View(model);
        }

        public IActionResult RegisterCompany(string userId)
        {
            return this.View();
        }

        public IActionResult ChangePassword()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await this.userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return this.View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return this.NotFound();
            }

            var user = await this.userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            var result = await this.userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return this.NotFound();
            }

            HttpContext.Session.SetString("userId", user.Id);

            if (user.LagunaRole == 1) return RedirectToAction("RegisterStudent", "Account");

            return RedirectToAction("RegisterCompany", "Account");
        }

        public IActionResult RecoverPassword()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
                    return this.View(model);
                }

                var myToken = await this.userHelper.GeneratePasswordResetTokenAsync(user);
                var link = this.Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);
                this.mailHelper.SendMail(model.Email, "LagunaLink Password Reset", $"<h1>LagunaLink Password Reset</h1>" +
                    $"To reset the password click in this link:</br></br>" +
                    $"<a href = \"{link}\">Reset Password</a>");
                this.ViewBag.Message = "The instructions to recover your password has been sent to email.";
                return this.View();

            }

            return this.View(model);
        }

        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await this.userHelper.GetUserByEmailAsync(model.UserName);
            if (user != null)
            {
                var result = await this.userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    this.ViewBag.Message = "Password reset successful.";
                    return this.View();
                }

                this.ViewBag.Message = "Error while resetting the password.";
                return View(model);
            }

            this.ViewBag.Message = "User not found.";
            return View(model);
        }


    }
}
