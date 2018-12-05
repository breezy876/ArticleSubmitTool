using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ArticleSubmitTool;
using ArticleSubmitTool.Code;
using ArticleSubmitTool.Code.Facebook;
using ArticleSubmitTool.Code.Filters;
using ArticleSubmitTool.Controllers;
using ArticleSubmitTool.Domain;
using ArticleSubmitTool.Domain.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ArticleSubmitTool.Models;
using ArticleSubmitTool.Models.InstantArticles;
using ArticleSubmitTool.Shared;
using ArticleSubmitTool.Web.Models.InstantArticles;
using Facebook;
using Newtonsoft.Json;

namespace ArticleSubmitTool.Controllers
{

    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //ViewBag.ReturnUrl = returnUrl;
            ViewBag.HideNavBars = true;

            if (SessionManager.IsLoggedIn)
            {
                return View(new LoginViewModel() { Email = SessionManager.User.Email, Password = SessionManager.User.Password});
            }

            if (returnUrl.IsNullOrEmpty())
            {
                returnUrl = "/Home/Index";
            }

            ViewBag.returnUrl = returnUrl;

            return View(new LoginViewModel());
        }

        [HttpPost]
        public JsonResult UpdateFacebookSessionInfo(string accessToken, string userId)
        {
            return Json(new { Success = FacebookHelpers.UpdateFacebookSessionInfo(accessToken, userId)});
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.HideNavBars = true;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true

            ApplicationUser user = UserManager.FindByEmail(model.Email);

            var result = await SignInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:

                    //save user info
                    var context = new DataContext();

                    var userRepos = new EFDataRepository<User>(context)
                    {
                        ExcludeDeleted = true,
                        IncludeRelated = true,
                        SaveChanges = true,
                        DeletePermanent = false
                    };

                    var savedUser = userRepos.Query(u => u.UserName == user.UserName).FirstOrDefault();

                    if (savedUser != null)
                    {
                        SessionManager.User = (UserModel)AutoMapper.Mapper.Map(savedUser, typeof(User), typeof(UserModel));
                    }

                    //create/retrieve user settings
                    var userSettingRepos = new EFDataRepository<UserSetting>(context)
                    {
                        ExcludeDeleted = true,
                        IncludeRelated = true,
                        SaveChanges = true,
                        DeletePermanent = false
                    };

                    var savedUserSettings = userSettingRepos.Query(u => u.UserId == savedUser.Id).FirstOrDefault();

                    if (savedUserSettings == null)
                    {
                        var newUserSettings = new UserSetting()
                        {
                            UserId = savedUser.Id,
                        };

                        SessionManager.UserSettings =
                            (UserSettingModel)
                                AutoMapper.Mapper.Map(newUserSettings, typeof (UserSetting), typeof (UserSettingModel));

                        userSettingRepos.AddOrUpdate(newUserSettings);
                    }
                    else
                    {
                        SessionManager.UserSettings = (UserSettingModel)
                                AutoMapper.Mapper.Map(savedUserSettings, typeof(UserSetting), typeof(UserSettingModel));
                    }

                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt. Email/Password incorrect or missing.");
                    return View(model);
            }
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public async Task<ContentResult> CreateAdmin()
        {

            if (UserManager.FindByName("admin") == null)
            {

                var user = new ApplicationUser { UserName = Settings.AdminUser, Email = Settings.AdminEmail };
                var result = await UserManager.CreateAsync(user, Settings.AdminPassword);

                if (result.Succeeded)
                {
                    result = UserManager.AddToRole(user.Id, "Admin");

                    var context = new DataContext();

                    var userRepos = new EFDataRepository<User>(context)
                    {
                        ExcludeDeleted = true,
                        IncludeRelated = true,
                        SaveChanges = true,
                        DeletePermanent = false
                    };

                    var savedUser = userRepos.Query(u => u.UserName == user.UserName).FirstOrDefault();

                    if (savedUser == null)
                    {

                        var newUser = new User()
                        {
                            UserName = Settings.AdminUser,
                            Email = Settings.AdminEmail,
                            Password = Settings.AdminPassword,
                            FirstName = Settings.AdminFirstName,
                            LastName = Settings.AdminLastName,
                            UserId = user.Id
                        };

                        userRepos.AddOrUpdate(newUser);
                    }

                    return Content(String.Format("Successfully created admin user\n\rUser Id: {0}.", user.Id));
                }
            }


            return Content(String.Format("Error creating admin user account."));

        }


        //
        // GET: /Account/Register
        [HttpPost]
        [AdminActionFilter]
        public async Task<ContentResult> CreateUser(CreateViewModel model)
        {
            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
            var result = await UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                result = UserManager.AddToRole(user.Id, "User");

                
                var context = new DataContext();

                var userRepos = new EFDataRepository<User>(context)
                {
                    ExcludeDeleted = true,
                    IncludeRelated = true,
                    SaveChanges = true,
                    DeletePermanent = false
                };

                var savedUser = userRepos.Query(u => u.UserName == user.UserName).FirstOrDefault();

                if (savedUser == null)
                {

                    var newUser = new User()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        Password = model.Password,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserId = user.Id
                    };

                    userRepos.AddOrUpdate(newUser);
                }

                return Content(String.Format("Successfully created user account.\n\rUser Id: {0}.", user.Id));
            }

            return Content(String.Format("Error creating user account."));
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLogin(string provider, string returnUrl, bool contentResult = false)
        {

            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }


        private ContentResult _ConvertToJSON(object obj)
        {

            var json = JsonConvert.SerializeObject(obj,
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Content(json, "application/json");
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            return RedirectToLocal(returnUrl);
        }

        //
        // POST: /Account/LogOff
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            SessionManager.User = null;
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
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