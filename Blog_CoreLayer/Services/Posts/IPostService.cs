using Blog_CoreLayer.DTO.Posts;
using Blog_CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Blog_CoreLayer.Services.Posts
{
    public interface IPostService
    {
        OperationResult CreatePost(CreatePostDto command);
        OperationResult EditPost(EditPostDto command);
        PostDto getPostById(int postId);
        PostDto getPostBySlug(string slug);
        PostFilterDto getPostByFilter(PostFilterParams FilterParams);
        bool IsSlugExist(string slug);
        List<PostDto> GetRelatedPosts(int GroupId);
        List<PostDto> GetPopularPost();
        void IncreaseVisit(int postId);
    }
}
