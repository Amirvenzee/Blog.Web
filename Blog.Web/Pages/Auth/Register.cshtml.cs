using Blog_CoreLayer.DTO.Users;
using Blog_CoreLayer.Services.Users;
using Blog_CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace Blog.Web.Pages.Auth
{
    [BindProperties]
    [ValidateAntiForgeryToken]
    public class RegisterModel : PageModel
    {

        private readonly IUserService _userService;

        #region Properties

        [Display(Name ="نام کاربری")]
        [Required(ErrorMessage = "*{0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage ="*{0} را وارد کنید")]        
        public string FullName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "*{0} را وارد کنید")]
        [MinLength(6,ErrorMessage = "{0} باید بیشتر از 6 کاراکتر باشد ")]
        public string Password { get; set; }


        #endregion


        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost() 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var Result = _userService.RegisterUser(new UserRegisterDto()
            {
                UserName = UserName,
                Password = Password,
                Fullname = FullName
            });
            if (Result.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError("UserName",Result.Message);
                return Page();
            }
            return RedirectToPage("Login");
        }
    }
}
