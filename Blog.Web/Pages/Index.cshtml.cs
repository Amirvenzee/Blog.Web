using Blog_CoreLayer.DTO;
using Blog_CoreLayer.DTO.Posts;
using Blog_CoreLayer.Services;
using Blog_CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Blog.Web.Pages
{

    public class IndexModel : PageModel
	{		
		private readonly IPostService _postService;
        private readonly IMainPageService _mainPageService;
        public IndexModel(IPostService postService, IMainPageService mainPageService)
        {

            _postService = postService;
            _mainPageService = mainPageService;
        }
        public MainPageDto MainPageData { get; set; }
        public void OnGet()
		{
            MainPageData = _mainPageService.GetData();
        }

        public IActionResult OnGetLatestPosts(string categorySlug)
        {
            var filterDto = _postService.getPostByFilter(new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = 1,
                Take = 6

            });

            return Partial("_LatestPosts",filterDto.Posts);
        }

		public IActionResult OnGetPopularPost()
		{
			return Partial("_PopularPosts", _postService.GetPopularPost());
		}
	}
}
