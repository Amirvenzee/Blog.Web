﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_CoreLayer.DTO.CategoryDto
{
    public class CategoryDto1
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string? MetaTag { get; set; }
        public string? MetaDescription { get; set; }
        public int? ParentId { get; set; }
    }

}

