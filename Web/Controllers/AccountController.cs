using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Web.ViewModels.Account;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmailService emailService;
        private readonly IUserRepository userRepository;

        public AccountController(IEmailService emailService, IUserRepository userRepository)
        {
            this.emailService = emailService;
            this.userRepository = userRepository;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            
            return View(new RegisterViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = await userRepository.GetExternalAuthenticationSchemesAsync()
            });
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            model.ExternalLogins = await userRepository.GetExternalAuthenticationSchemesAsync();
            
            if(ModelState.IsValid)
            {
                var result = await userRepository.GetCreateResultAsync(model.Email, model.UserName, model.Password);
                var user = await userRepository.FindByNameAsync(model.UserName);
                if (result.Succeeded)
                {
                    var code = await userRepository.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    await emailService.SendEmailAsync(model.Email, "Confirm your account",
                        $"Confirm your registration by clicking the link: <a href='{callbackUrl}'>link</a>");
 
                    return View("Email");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await userRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await userRepository.GetConfirmEmailResultAsync(user, code);
            if(result.Succeeded)
                return RedirectToAction("Index", "Home");
            return View("Error");
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = await userRepository.GetExternalAuthenticationSchemesAsync()
            });
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            model.ExternalLogins = await userRepository.GetExternalAuthenticationSchemesAsync();
            
            if (ModelState.IsValid)
            {
                var user = await userRepository.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (!await userRepository.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "You didn't confirm your email");
                        return View(model);
                    }
                }
 
                var result = await userRepository.PasswordSignInAsync(model.UserName, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect login or password");
                }
            }
            return View(model);
        }
        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
                new {ReturnUrl = returnUrl});
            var properties = userRepository.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = await userRepository.GetExternalAuthenticationSchemesAsync()
            };
            
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", loginViewModel);
            }

            var info = await userRepository.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information");

                return View("Login", loginViewModel);
            }

            var signInResult = await userRepository.ExternalLoginSignInAsync(info);

            if (signInResult.Succeeded)
            {
                return Redirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    await userRepository.ExternalAuthenticationLogin(email, info);
                    
                    return Redirect(returnUrl);
                }

                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on WebAppProg@gmail.com";

                return View("Error");
            }
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await userRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}