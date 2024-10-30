using Blog_CoreLayer.DTO.Comments;
using Blog_CoreLayer.Utilities;
using Blog_DataLayer.Context;
using Blog_DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_CoreLayer.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly BlogContext _blogContext;

        public CommentService(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public OperationResult CreateComment(CreateCommentDto command)
        {
            var comment = new PostComment()
            {
                PostId = command.PostId,
                UserId = command.UserId,
                Text = command.Text,
                
            };
            _blogContext.Add(comment);
            _blogContext.SaveChanges();
            return OperationResult.Success();


        }

        public List<CommentDto> GetPostComments(int postId)
        {
            return _blogContext.PostComments
                .Include(x=>x.User)
                .Where(x => x.PostId == postId)
                .Select(comment => new CommentDto()
                {
                    PostId =comment.PostId,
                    Text = comment.Text,
                    UserFullName = comment.User.FullName,
                    CommentId = comment.Id,
                    CreateDate = comment.CreateDate,
                    
                }).ToList();
        }
    }
}
