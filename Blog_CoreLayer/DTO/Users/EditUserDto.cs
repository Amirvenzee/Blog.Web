using Blog_DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_CoreLayer.DTO.Users
{
    public class EditUserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public UserRole Role { get; set; }
    }
}
