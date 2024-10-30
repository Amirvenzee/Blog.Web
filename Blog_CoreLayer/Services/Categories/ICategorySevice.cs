using Blog_CoreLayer.DTO.CategoryDto;
using Blog_CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_CoreLayer.Services.Categories
{
    public interface ICategorySevice
    {
        OperationResult CreateCategory(CreateCategoryDto create);
        OperationResult EditCategory(EditCategoryDto create);
        List<CategoryDto1> GetAllCategory();
        List<CategoryDto1> GetChildCategories(int parentId);
        CategoryDto1 GetCategoryById(int id);
        CategoryDto1 GetCategoryBySlug(string slug);
        bool IsSlugExist(string slug);
            
    }
}
