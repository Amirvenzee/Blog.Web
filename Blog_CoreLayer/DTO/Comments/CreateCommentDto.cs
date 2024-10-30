using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_CoreLayer.DTO.Comments
{
    public class CreateCommentDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
    }
}
