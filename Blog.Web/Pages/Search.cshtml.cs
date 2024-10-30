using Blog_CoreLayer.DTO.Posts;
using Blog_CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IPostService _postService;

        public SearchModel(IPostService postService)
        {
            _postService = postService;
        }

        public PostFilterDto Filter { get; set; }
        public void OnGet(int pageId=1, string categorySlug=null,string q=null)
        {
            Filter = _postService.getPostByFilter(new PostFilterParams()
            {
               CategorySlug = categorySlug,
               PageId = pageId,
               Take = 2,
               Title = q

            });
        }

        public IActionResult OnGetPagination( int pageId = 1, string categorySlug = null, string q = null)
        {
            var model = _postService.getPostByFilter(new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Take = 2,
                Title = q
            });
            return Partial("_SearchView", model);
        }
    }
}
