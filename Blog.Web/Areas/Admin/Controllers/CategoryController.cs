using Blog.Web.Areas.Admin.Models.Categories;
using Blog_CoreLayer.DTO.CategoryDto;
using Blog_CoreLayer.Services.Categories;
using Blog_CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
   
    public class CategoryController : AdminControllerBase
    {
        private readonly ICategorySevice _categorySevice;

        public CategoryController(ICategorySevice categorySevice)
        {
            _categorySevice = categorySevice;
        }
        
        public IActionResult Index()
        {
            return View(_categorySevice.GetAllCategory());
        }

        [Route("/Admin/Category/Add/{parentId?}")]
        public IActionResult Add(int? parentId)
        {
            return View();
        }

        [HttpPost("/Admin/Category/Add/{parentId?}")]
        public IActionResult Add(int? parentId, CreateCategoryViewModel createViewModel)
        {
            createViewModel.ParentId = parentId;
            var result = _categorySevice.CreateCategory(createViewModel.MapToDto());

            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(createViewModel.Slug),result.Message);
                return View();
            }        
            return RedirectToAction("Index");
          
        }
 
        public IActionResult Edit(int id)
        {
            var category = _categorySevice.GetCategoryById(id);
            if (category == null)
                return RedirectToAction("Index");

            var model = new EditCategoryViewModel
            {
                Slug = category.Slug,
                MetaTag = category.MetaTag,
                MetaDescription = category.MetaDescription,
                Title = category.Title
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,EditCategoryViewModel model)
        {
            var Result = _categorySevice.EditCategory(new EditCategoryDto()
            {
                Slug = model.Slug,
                MetaTag = model.MetaTag,
                MetaDescription = model.MetaDescription,
                Title = model.Title,
                Id= id

            });
            if(Result.Status!=OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(model.Slug),Result.Message);
                return View();
            }
                
            return RedirectToAction("Index");
        }
         
        public IActionResult GetChildCategories(int parentId)
        {
            var category = _categorySevice.GetChildCategories(parentId);

            return new JsonResult(category);
        }

    }
}
