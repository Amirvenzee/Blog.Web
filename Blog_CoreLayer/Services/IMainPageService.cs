using Blog_CoreLayer.DTO;
using Blog_CoreLayer.Mappers;
using Blog_CoreLayer.Services.Posts;
using Blog_DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_CoreLayer.Services
{
    public interface IMainPageService
    {
        MainPageDto GetData();
    }

    public class MainPageService : IMainPageService
    {
        private readonly BlogContext _context;
        public MainPageService(BlogContext context)
        {
            _context = context;
        }

        public MainPageDto GetData()
        {
            var categories = _context.Categories
                .OrderByDescending(x => x.Id)
                .Take(6)
                .Include(x => x.Posts)
                .Include(s => s.SubPosts)
                .Select(category => new MainPageCategoryDto()
                {
                    Title = category.Title,
                    Slug = category.Slug,
                    PostChild = category.Posts.Count + category.SubPosts.Count,
                    IsMainCategory = category.ParentId == null

                }).ToList();

            var specialPosts = _context.Posts
                .OrderByDescending(x => x.Id)
                .Include(x => x.Category)
                .Include(c => c.SubCategory)
                .Where(r => r.IsSpecial).Take(4)
                .Select(post => PostMapper.MaptoDto(post)).ToList();

            var latestPosts = _context.Posts
                .Include(x => x.Category)
                .Include(c => c.SubCategory)
                .OrderByDescending(x => x.Id)
                .Take(6).Select(post => PostMapper.MaptoDto(post)).ToList();

            return new MainPageDto()
            {
                LatestPosts = latestPosts,
                Categories = categories,
                SpecialPosts = specialPosts
            };

        }
    }
}
