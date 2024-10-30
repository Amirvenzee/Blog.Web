using Blog_DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_CoreLayer.DTO.Users
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string PassWord { get; set; }
        public UserRole Role { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
