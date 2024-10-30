using Blog_CoreLayer.DTO.Comments;
using Blog_CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_CoreLayer.Services.Comments
{
    public interface ICommentService
    {
        OperationResult CreateComment(CreateCommentDto command);
        List<CommentDto> GetPostComments(int PostId);
    }
}
