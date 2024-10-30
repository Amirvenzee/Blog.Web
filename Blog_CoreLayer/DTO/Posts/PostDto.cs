using Blog_CoreLayer.DTO.CategoryDto;

namespace Blog_CoreLayer.DTO.Posts
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string UserFullName { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public int Visit { get; set; }
        public bool IsSpecial { get; set; }
        


        public DateTime CreateDate { get; set; }
        public CategoryDto1 Category { get; set; }
        public CategoryDto1 SubCategory { get; set; }
    }
}

