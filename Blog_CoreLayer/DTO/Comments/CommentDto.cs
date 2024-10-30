using System.ComponentModel.DataAnnotations;

namespace Blog_CoreLayer.DTO.Comments
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string UserFullName { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
