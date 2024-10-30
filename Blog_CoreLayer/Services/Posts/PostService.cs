using Blog_CoreLayer.DTO.Posts;
using Blog_CoreLayer.Mappers;
using Blog_CoreLayer.Services.FileManager;
using Blog_CoreLayer.Utilities;
using Blog_DataLayer.Context;
using Blog_DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_CoreLayer.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly BlogContext _context;
        private readonly IFileManager _FileManager;
        public PostService(BlogContext result, IFileManager fileManager)
        {
            _context = result;
            _FileManager = fileManager;
        }
        public OperationResult CreatePost(CreatePostDto command)
        {
            if (command.ImageFile == null)
                return OperationResult.Error();

            var post = PostMapper.MapCreateDtoToPost(command);

            if (IsSlugExist(post.Slug))
                return OperationResult.Error("Slug تکراری است");

            post.ImageName = _FileManager.SaveImageAndReturnImageName(command.ImageFile, Directories.PostImage);
            _context.Posts.Add(post);
            _context.SaveChanges();

            return OperationResult.Success();
        }

        public OperationResult EditPost(EditPostDto command)
        {
            var post = _context.Posts.FirstOrDefault(x=>x.Id ==command.PostId);
            var oldImage = post.ImageName;
            if (post == null)
               return OperationResult.NotFound();

              var newSlug = command.Slug.ToSlug();

            if (post.Slug != newSlug)
                if (IsSlugExist(newSlug))
                    return OperationResult.Error("Slug تکراری است");

            PostMapper.EditPost(command, post);
            if (command.ImageFile != null)
                post.ImageName = _FileManager.SaveImageAndReturnImageName(command.ImageFile,Directories.PostImage);

            _context.SaveChanges();
            if (command.ImageFile != null)
                _FileManager.DeleteFile(oldImage,Directories.PostImage);
                return OperationResult.Success();
        }

        public List<PostDto> GetPopularPost()
        {
            return _context.Posts.Include(x=>x.User)
              .OrderByDescending(x => x.Visit)
              .Take(6).Select(post => PostMapper.MaptoDto(post)).ToList();
        }

        public PostFilterDto getPostByFilter(PostFilterParams filterParams)
        {
            var result = _context.Posts
                .Include(d => d.Category)
                .Include(d => d.SubCategory)
                .OrderByDescending(d => d.CreateDate)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.CategorySlug))
                result = result.Where(r => r.Category.Slug == filterParams.CategorySlug
                                           || r.SubCategory.Slug == filterParams.CategorySlug);

            if (!string.IsNullOrWhiteSpace(filterParams.Title))
                result = result.Where(r => r.Title.Contains(filterParams.Title));

            var skip = (filterParams.PageId - 1) * filterParams.Take;
            var model = new PostFilterDto()
            {
                Posts = result.Skip(skip).Take(filterParams.Take)
                    .Select(post => PostMapper.MaptoDto(post)).ToList(),
                FilterParams = filterParams,
            };
            model.GeneratePaging(result, filterParams.Take, filterParams.PageId);

            return model;
        }

        public PostDto getPostById(int postId)
        {
            var post = _context.Posts
                .Include(x=>x.SubCategory)
                .Include(x=>x.Category)
                .FirstOrDefault(x => x.Id ==postId);

            return PostMapper.MaptoDto(post);
        }

        public PostDto getPostBySlug(string slug)
        {

            var post = _context.Posts
               .Include(x => x.SubCategory)
            .Include(x => x.Category)
            .Include(x=>x.User)
               .FirstOrDefault(x => x.Slug == slug);

            if (post == null)
                return null;

            return PostMapper.MaptoDto(post);
        }

        public List<PostDto> GetRelatedPosts(int categoryId)
        {
            return _context.Posts.Where
                (x => x.CategoryId == categoryId ||x.SubCategoryId == categoryId)
                .OrderByDescending(x => x.CreateDate)
                .Take(6).Select(post=>PostMapper.MaptoDto(post)).ToList();
        }

        public void IncreaseVisit(int postId)
        {
            var post = _context.Posts.First(x=>x.Id==postId);
            post.Visit += 1;
            _context.SaveChanges();
        }

        public bool IsSlugExist(string slug)
        {
            return _context.Posts.Any(x => x.Slug == slug.ToSlug());
        }
    }
}
