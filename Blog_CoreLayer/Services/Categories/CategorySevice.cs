using Blog_CoreLayer.DTO.CategoryDto;
using Blog_CoreLayer.Mappers;
using Blog_CoreLayer.Utilities;
using Blog_DataLayer.Context;
using Blog_DataLayer.Entities;

namespace Blog_CoreLayer.Services.Categories
{
    public class CategorySevice : ICategorySevice
    {
        private readonly BlogContext _blogContext;

        public CategorySevice(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public OperationResult CreateCategory(CreateCategoryDto create)
        {
            if (IsSlugExist(create.Slug))
                return OperationResult.Error("Slug Is Exist");

            var category = new Category() 
            {
                Title= create.Title,
                Slug= create.Slug.ToSlug(),
                MetaTag= create.MetaTag,
                MetaDescription= create.MetaDescription,
                IsDelete = false,
                ParentId= create.ParentId
                
            };
            _blogContext.Categories.Add(category);
            _blogContext.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult EditCategory(EditCategoryDto create)
        {
            var category = _blogContext.Categories.FirstOrDefault(x => x.Id == create.Id);
            if (category == null)
                return OperationResult.NotFound();

            if(create.Slug.ToSlug()!=category.Slug)
                if (IsSlugExist(create.Slug))
                    return OperationResult.Error("Slug Is Exist");


            category.Title = create.Title;
            category.Slug = create.Slug.ToSlug();
            category.MetaTag = create.MetaTag;
            category.MetaDescription = create.MetaDescription;

            _blogContext.SaveChanges();

            return OperationResult.Success();
        }

        public List<CategoryDto1> GetAllCategory()
        {
            
            return _blogContext.Categories.Select(category=>CategoryMapper.MapToDto(category)).ToList();
        }

        public List<CategoryDto1> GetChildCategories(int parentId)
        {
            return _blogContext.Categories.Where(w=>w.ParentId ==parentId)
                .Select(category => CategoryMapper.MapToDto(category)).ToList();
        }

        public CategoryDto1 GetCategoryById(int id)
        {
            var category = _blogContext.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
                return null;
            return CategoryMapper.MapToDto(category);

           
        }

        public CategoryDto1 GetCategoryBySlug(string slug)
        {
            var category = _blogContext.Categories.FirstOrDefault(x => x.Slug == slug);

            if (category == null)
                return null;
            return CategoryMapper.MapToDto(category);

        }

        public bool IsSlugExist(string slug)
        {
            return _blogContext.Categories.Any(x => x.Slug == slug);
        }
    }
}
