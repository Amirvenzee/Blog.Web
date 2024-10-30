using Blog_DataLayer.Entities;
using Blog_CoreLayer.DTO.Users;
using Blog_CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_CoreLayer.Services.Users
{
    public interface IUserService
    {
        OperationResult EditUser(EditUserDto command);
        OperationResult RegisterUser(UserRegisterDto registerDto);
        UserDto LoginUser(LoginUserDto loginUserDto); 
        UserDto GetUserById(int userId);
        UserFilterDto GetUsersByFilter(int pageId, int take);

    }
}
