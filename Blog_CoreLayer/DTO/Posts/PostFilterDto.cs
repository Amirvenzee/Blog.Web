using Blog_CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_CoreLayer.DTO.Posts
{
    public class PostFilterDto:BasePagination
    {
       
        public List<PostDto> Posts { get; set; }
        public PostFilterParams FilterParams { get; set; }

    }
     public class PostFilterParams
    {
        public int PageId { get; set; }
        public int Take { get; set; }
        public string Title { get; set; }
        public string CategorySlug { get; set; }
    }
}
