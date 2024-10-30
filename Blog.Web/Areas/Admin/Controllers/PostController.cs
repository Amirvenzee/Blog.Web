using Blog.Web.Areas.Admin.Models.Posts;
using Blog_CoreLayer.DTO.Posts;
using Blog_CoreLayer.Services.Posts;
using Blog_CoreLayer.Utilities;
using CodeYad_Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
   
   
    public class PostController : AdminControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index(int pageId=1,string title="", string categorySlug = "")
        {
            var param = new PostFilterParams()
            {
                CategorySlug = categorySlug,
                Title = title,
                PageId = pageId,
                Take = 3
            };
            var model = _postService.getPostByFilter(param);
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreatePostViewModel createViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createViewModel);
            }

            var result = _postService.CreatePost(new CreatePostDto()
            {
                CategoryId = createViewModel.CategoryId,
                Description = createViewModel.Description,
                ImageFile = createViewModel.ImageFile,
                Slug = createViewModel.Slug,
                SubCategoryId = createViewModel.SubCategoryId == 0 ? null :createViewModel.SubCategoryId,
                Title = createViewModel.Title,
                IsSpecial = createViewModel.IsSpecial,
                UserId = User.GetUserId()
            });

            if (result.Status != OperationResultStatus.Success)
            {

                ModelState.AddModelError(nameof(CreatePostViewModel.Slug), result.Message);
                return View(createViewModel);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int Id)
        {
            var post = _postService.getPostById(Id);
            if (post == null)
                return RedirectToAction("Index");

            var model = new EditPostViewModel()
            {
                CategoryId = post.CategoryId,
                Description = post.Description,
                Title = post.Title,
                Slug = post.Slug,
                SubCategoryId = post.SubCategoryId,
                IsSpecial= post.IsSpecial,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id,EditPostViewModel editViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editViewModel);
            }

            var result = _postService.EditPost(new EditPostDto()
            {
                CategoryId = editViewModel.CategoryId,
                Description = editViewModel.Description,
                ImageFile = editViewModel.ImageFile,
                Slug = editViewModel.Slug,
                SubCategoryId = editViewModel.SubCategoryId == 0 ? null : editViewModel.SubCategoryId,
                Title = editViewModel.Title,
                PostId = Id,
                IsSpecial = editViewModel.IsSpecial,
                

            });

            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(CreatePostViewModel.Slug), result.Message);
                return View(editViewModel);
            }
            return RedirectToAction("Index");
        }
    }
}
