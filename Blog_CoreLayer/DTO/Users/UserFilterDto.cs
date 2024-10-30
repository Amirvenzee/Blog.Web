using Blog_CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_CoreLayer.DTO.Users
{
    public class UserFilterDto:BasePagination
    {
        public List<UserDto> Users { get; set; }
    }
}
