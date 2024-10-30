using Blog_CoreLayer.DTO.Posts;
using Blog_CoreLayer.Utilities;
using Blog_DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_CoreLayer.Mappers
{
    public class PostMapper
    {
        public static Post MapCreateDtoToPost(CreatePostDto dto)
        {
            return new Post
            {
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                Slug = dto.Slug.ToSlug(),
                Title = dto.Title,
                UserId = dto.UserId,
                Visit = 0,
                IsDelete = false,
                IsSpecial = dto.IsSpecial,
                SubCategoryId = dto.SubCategoryId
            };
        }

        public static PostDto MaptoDto(Post post)
        {
    
            return new PostDto
            {
                Description = post.Description,
                CategoryId = post.CategoryId,
                Slug = post.Slug,
                Title = post.Title,
                Visit = post.Visit,
                CreateDate = post.CreateDate,
                Category = post.Category == null? null : CategoryMapper.MapToDto(post.Category),
                ImageName = post.ImageName,
                UserFullName=post.User?.FullName,
                PostId= post.Id,
                SubCategoryId= post.SubCategoryId,
                SubCategory = post.SubCategory == null?null:CategoryMapper.MapToDto(post.SubCategory),
                IsSpecial = post.IsSpecial
            };
        }

        public static Post EditPost(EditPostDto dto, Post post)
        {
            post.Description = dto.Description;
            post.CategoryId = dto.CategoryId;
            post.Slug = dto.Slug.ToSlug();
            post.Title = dto.Title;
            post.SubCategoryId = dto.SubCategoryId;
            post.IsSpecial = dto.IsSpecial;
            return post;
        } 
    }
}
