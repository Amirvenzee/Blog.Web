using Blog_CoreLayer.DTO.Users;
using Blog_CoreLayer.Services.Users;
using Blog_CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    public class UserController : AdminControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(int pageId = 1)
        {
            return View(_userService.GetUsersByFilter(pageId,10));
        }
        [HttpPost]
        public IActionResult Edit(EditUserDto editModel)
        {
            var result = _userService.EditUser(editModel);
            if (result.Status != OperationResultStatus.Success)
            {
                ErrorAlert(result.Message);
                return RedirectToAction("Index");
            }
            //  return RedirectAndShowAlert(result, RedirectToAction("Index"));
            return RedirectToAction("Index");
        }
        public IActionResult ShowEditModal(int userId)
        {
            var user = _userService.GetUserById(userId);
            return PartialView("_EditUser", new EditUserDto()
            {
                FullName = user.FullName,
                Role = user.Role,
                UserId = userId
            });
        }
    }
}
