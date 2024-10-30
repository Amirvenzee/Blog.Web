using Blog_CoreLayer.Services.FileManager;
using Blog_CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    public class UploadController : Controller
    {
        private readonly IFileManager _FileManager;

        public UploadController(IFileManager fileManager)
        {
            _FileManager = fileManager;
        }
        [Route("/Upload/Article")]
        public IActionResult UploadArticleImage(IFormFile upload)
        {
            if (upload == null)
                BadRequest();
            var fileName = _FileManager.SaveFileAndReturnName(upload,Directories.PostContentImage);

            return  Json(new {Uploaded = true, Url = Directories.GetPostContentImage(fileName)});
        }
    }
}
