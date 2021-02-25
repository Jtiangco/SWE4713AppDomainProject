using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Project_for_App_Domain.Models;
using Project_for_App_Domain.Helpers;

namespace Project_for_App_Domain.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                //Validating the user, whether the user is valid or not.
                var isValidUser = IsValidUser(model);

                //If user is valid & present in database, we are redirecting it to Welcome page.
                if (isValidUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index");
                }
                else
                {
                    IncrementLoginFailure(model.Username);
                    //If the username and password combination is not present in DB then error message is shown.
                    ModelState.AddModelError("Failure", "Wrong Username and password combination !");
                    return View();
                }
            }
            else
            {
                //If model state is not valid, the model with error message is returned to the View.
                return View(model);
            }
        }

        public User IsValidUser(LoginViewModel model)
        {
            using (var dbContext = new SWE4713Entities())
            {
                //Retireving the user details from DB based on username and password enetered by user.
                User user = dbContext.Users.Where(query => query.UserName.Equals(model.Username) && query.Password.Equals(Hasher.HashString(model.Password))).SingleOrDefault();
                //If user is present, then true is returned.
                if (user == null)
                    return null;
                //If user is not present false is returned.
                else
                    return user;
            }
        }

        private bool IncrementLoginFailure(string username)
        {
            bool result = false;

            using (var dbContext = new SWE4713Entities())
            {
                var usr = dbContext.Users.SingleOrDefault(b => b.UserName == username);
                if (usr != null)
                {
                    var loginAttempts = usr.Attempts;
                    if (loginAttempts < 3)
                    {
                        usr.Attempts += 1;
                        dbContext.SaveChanges();
                        result = true;
                    }
                }
            }
            return result;
        }
        private bool ResetLoginFailure(string email)
        {
            bool result = false;

            using (var dbContext = new SWE4713Entities())
            {
                var usr = dbContext.Users.SingleOrDefault(b => b.Email == email);
                if (usr != null)
                {
                    usr.Attempts = 0;
                    dbContext.SaveChanges();
                    result = true;
                }
            }
            return result;
        }

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var dbContext = new SWE4713Entities())
                {
                    User db = new User();

                    db.FirstName = model.FirstName;
                    db.LastName = model.LastName;
                    db.DOB = model.DOB;
                    db.Street = model.Street;
                    db.City = model.City;
                    db.State = model.State;
                    db.Zip = model.Zip;
                    db.UserTypeId = 3; //default set to Accountant
                    db.UserName = model.FirstName.Substring(0, 1).ToLower() + model.LastName.ToLower() + DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString().Substring(2, 2); //needs to save it as [firstinitial][lastname][month][year]
                    //db.Picture = Convert.ToByte(model.Picture);
                    db.Email = model.Email;
                    db.DateCreated = DateTime.Now;
                    db.Active = true;
                    db.Password = Hasher.HashString(model.Password);

                    dbContext.Users.Add(db);
                    dbContext.SaveChanges();

                }
                ViewBag.Message = "User Details Saved";
                return View("Register");
            }
            else
            {
                return View("Register", model);
            }
        }

        // GET: /Account/ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            EmailHelper eh = new EmailHelper();
            if (ModelState.IsValid)
            {
                //verify email address in database
                using (var dbContext = new SWE4713Entities())
                {
                    User usr = (from s in dbContext.Users
                                   where s.Email == model.Email
                                   select s).FirstOrDefault<User>();
                    if (usr != null)
                    {
                        string code = GeneratePasswordToken(usr.UserId);
                        // send email with return url and code
                        var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = usr.UserId, code = code }, protocol: Request.Url.Scheme);
                        EmailModel em = new EmailModel();
                        em.SendFrom = "atiangco94@gmail.com";
                        em.SendTo = usr.Email;
                        em.Subject = "Forgot Password Reset";
                        em.EmailBody = "<br>" + "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>";
                        eh.SendEmail(em);
                    }
                }
                ViewBag.Message = "Password Reset Email Sent Successfully.";
                return View("ForgotPasswordConfirmation", "Account");
            }
            else
            {
                // If we got this far, something failed, redisplay form
                return View("ForgotPasswordConfirmation", model);
            }
        }

        private string GeneratePasswordToken(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            Random _random = new Random();

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        // GET: /Account/ForgotPasswordConfirmation
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ResetPassword
        public ActionResult ResetPassword(string code)
        {
            return View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                using (var dbContext = new SWE4713Entities())
                {
                    Models.User userdb = (from s in dbContext.Users
                                          where s.Email == model.Email
                                          select s).FirstOrDefault<User>();
                    var oldPassword = userdb.Password;

                    PasswordHistory phistory = new PasswordHistory();

                    phistory.OldPassword = oldPassword;
                    phistory.DateCreated = DateTime.Now;
                    phistory.Active = true;
                    phistory.UserId = userdb.UserId;
                    dbContext.PasswordHistories.Add(phistory);
                    dbContext.SaveChanges();

                    var usr = dbContext.Users.SingleOrDefault(b => b.Email == model.Email);
                    if (usr != null)
                    {
                        usr.Attempts = 0;
                        usr.DateUpdated = DateTime.Now;
                        dbContext.SaveChanges();
                    }

                    ResetLoginFailure(model.Email);
                }
                return View("ResetPasswordConfirmation", "Account");
            }
            else
            {
                return View("ResetPasswordConfirmation", model);
            }
        }

        // GET: /Account/ResetPasswordConfirmation
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // POST: /Account/LogOff
        [HttpPost]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }



        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}