using Blog_CoreLayer.DTO.CategoryDto;
using Blog_DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_CoreLayer.Mappers
{
    public class CategoryMapper
    {
        public static CategoryDto1 MapToDto(Category category)
        {
            return new CategoryDto1()
            {
                MetaDescription = category.MetaDescription,
                MetaTag = category.MetaTag,
                Slug = category.Slug,
                Title = category.Title,
                ParentId = category.ParentId,
                Id = category.Id
            };
        }
    }
}
