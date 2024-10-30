using Blog_CoreLayer.DTO.Users;
using Blog_CoreLayer.Services.Users;
using Blog_CoreLayer.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Blog.Web.Pages.Auth
{
    [ValidateAntiForgeryToken]
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;


        [Required(ErrorMessage = "**نام کاربری را وارد کنید**")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "**کلمه عبور را وارد کنید**")]
        [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 6 کاراکتر باشد ")]
        public string Password { get; set; }

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }




        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid==false)
            {
                return Page();
            }

            var user = _userService.LoginUser( new LoginUserDto 
            {
                Password = Password,
                UserName= UserName
            });
            if (user == null)
            {
                ModelState.AddModelError("UserName", "کاربری با این مشخصات یافت نشد");
                return Page();
            }


            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.FullName),
                new Claim(ClaimTypes.Role,user.Role.ToString())

            };
            var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);
            //اطلاعاتی که باهاش لاگین کردی ذخیره میشه و بعد بستن مرورگر میتونی دوباری وارد صفحه اصلی بشی
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            HttpContext.SignInAsync(claimPrincipal,properties);
            return RedirectToPage("../index");
        }
    }
}
