using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SK_PG_WebApp.DAL;
using SK_PG_WebApp.Helper;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SK_PG_WebApp.Controllers
{
    public class UMController : Controller
    {
        private readonly ILogger<UMController> _logger;
        private readonly IConfiguration config;
        private readonly DatabaseContext dbContext;

        public UMController(ILogger<UMController> logger, DatabaseContext databaseContext, IConfiguration _config)
        {
            _logger = logger;
            dbContext = databaseContext;
            config = _config;
        }


        public IActionResult Login()
        {
            ViewBag.error = string.Empty;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            try
            {
                ViewBag.error = string.Empty;
                if (userName.IsNotNullOrEmpty() && password.IsNotNullOrEmpty())
                {
                    var data = dbContext.UsersDbSet.Where(x => x.email == userName && x.password == password).FirstOrDefault();
                    if (data.IsNotNull())
                    {
                        #region Cookie Identity Creation

                        var identity = new ClaimsIdentity(new[] {
                                     new Claim(ClaimTypes.Name, Convert.ToString(data.id)),
                                      new Claim(ClaimTypes.Role, Convert.ToString(data.roleId)),
                                      new Claim(ClaimTypes.Actor, Convert.ToString(data.name)),
                                      new Claim("UserName", Convert.ToString(data.name)),
                                      new Claim("Designation", Convert.ToString(data.designation)),
                                      new Claim("EmailAddress", Convert.ToString(data.email)),
                                      new Claim("UserId", Convert.ToString(data.id)),
                                      new Claim("roleId", Convert.ToString(data.roleId)),
                                      new Claim("mobile", Convert.ToString(data.whatsAppNumber)),
                                      new Claim("isSAVisible",Convert.ToInt32(data.roleId) == 1 ? "block" : "none")
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                        var principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal
                            , new AuthenticationProperties()
                            {
                                IsPersistent = true
                            });


                        #endregion

                        return RedirectToAction("Index1", "Home");
                    }
                }
                ViewBag.error = "Invalid Username or password";
                
            }
            catch(Exception ex)
            {
                ViewBag.error = "Something went wrong, Please contact to Admin";
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut    
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page    
            return RedirectToAction("Login");
        }
    }
}
