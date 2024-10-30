using Blog_DataLayer.Context;
using Blog_DataLayer.Entities;
using Blog_CoreLayer.DTO.Users;
using Blog_CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blog_CoreLayer.Services.Users
{
    public class UserSevice : IUserService
    {
        private readonly BlogContext _Context;

        public UserSevice(BlogContext context)
        {
            _Context = context;
        }

        public OperationResult EditUser(EditUserDto command)
        {
            var user = _Context.Users.FirstOrDefault(x => x.Id == command.UserId);
            if (user == null)
                return OperationResult.NotFound();

           user.Role = command.Role;
            user.FullName = command.FullName;
            _Context.SaveChanges();
            return OperationResult.Success();
        }

        public UserDto GetUserById(int userId)
        {
            var model = _Context.Users.FirstOrDefault(x=>x.Id == userId);
            if (model == null)
                return null;

            return new UserDto()
            {
                UserId = model.Id,
                FullName = model.FullName,
                Role = model.Role,
                PassWord = model.PassWord,
                Username = model.Username,
                RegisterDate = model.CreateDate
            };
        }

        public UserFilterDto GetUsersByFilter(int pageId, int take)
        {
            var users = _Context.Users.OrderByDescending(d => d.Id)
                .Where(c => !c.IsDelete);

            var skip = (pageId - 1) * take;
            var model = new UserFilterDto()
            {
                Users = users.Skip(skip).Take(take).Select(user => new UserDto()
                {
                    FullName = user.FullName,
                    PassWord = user.PassWord,
                    Role = user.Role,
                    Username = user.Username,
                    RegisterDate = user.CreateDate,
                    UserId = user.Id
                }).ToList()
            };
            model.GeneratePaging(users, take, pageId);
            return model;
        }

        public UserDto LoginUser(LoginUserDto loginUserDto)
        {
            var PassWordHashed = loginUserDto.Password.EncodePassword();
            var user = _Context.Users
                .FirstOrDefault(x => x.Username == loginUserDto.UserName && x.PassWord == PassWordHashed);

            if (user == null)
                return null;

            var userDto = new UserDto() 
            {
                FullName = user.FullName,
                PassWord = user.PassWord,
                Role = user.Role,
                Username = user.Username,
                RegisterDate = user.CreateDate,
                UserId = user.Id,
                
            };
            return userDto;

               
           
            
                
            

        }

        public OperationResult RegisterUser(UserRegisterDto registerDto)
        {
            var isUserNameExist = _Context.Users.Any(x=>x.Username==registerDto.UserName);
            if (isUserNameExist)
                return OperationResult.Error("*نام کاربری تکراری است*");

            var PasswordHash = registerDto.Password.EncodePassword();
            _Context.Users.Add(new User
            {
                FullName = registerDto.Fullname,
                Username = registerDto.UserName,
                PassWord = PasswordHash,
                IsDelete=false,
                CreateDate = DateTime.Now,
                Role=UserRole.User
                
            });
            _Context.SaveChanges();
            return OperationResult.Success();
            
        }
    }
}
