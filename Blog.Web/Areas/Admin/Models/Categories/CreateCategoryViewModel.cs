using Blog_CoreLayer.DTO.CategoryDto;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Areas.Admin.Models.Categories
{
    public class CreateCategoryViewModel
    {

        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="وارد کردن {0} اجباری است")]

        public string Title { get; set; }

        [Display(Name ="Slug")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]

        public string Slug { get; set; }

        [Display(Name ="Metatag(با - از هم جدا کنید )")]
        public string? MetaTag { get; set; }


        [DataType(DataType.MultilineText)]
        public string? MetaDescription { get; set; }
        public int? ParentId { get; set; }


        public CreateCategoryDto MapToDto()
        {
            return new CreateCategoryDto
            {
                Title = Title,
                Slug = Slug,
                MetaTag = MetaTag,
                MetaDescription = MetaDescription,
                ParentId = ParentId
            };

        }

    }



}
