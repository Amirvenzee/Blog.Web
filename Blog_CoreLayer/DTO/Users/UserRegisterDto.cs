using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_CoreLayer.DTO.Users
{
    public class UserRegisterDto
    {
        public string Fullname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
