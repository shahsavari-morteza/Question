using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quiz.Application.Contracts.Identity;
using Quiz.Application.Exceptions;
using Quiz.Application.Models.Authentication;
using Quiz.Application.Models.Authentication.DTO.Users;
using Quiz.Application.Response;
using Quiz.Identity.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Identity.Services
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly UserManager<ApplicationUser> userManager;
       // private readonly RoleManager<ApplicationRole> roleManager;
        private readonly QuizIdentityDbContext db;
        public UserIdentityService(UserManager<ApplicationUser> userManager, QuizIdentityDbContext db /*,RoleManager<ApplicationRole> roleManager*/)
        {
            this.userManager = userManager;
            //this.roleManager = roleManager;
            this.db= db;
        }
        public async Task<AddNewUserResponse> AddNewUser(RegistrationRequest user)
        {
            AddNewUserResponse response = new AddNewUserResponse();
            ApplicationUser newuser = new ApplicationUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
            };
            var result=await userManager.CreateAsync(newuser,user.Password);
            if(result.Succeeded)
            {
               
                response.UserId = newuser.Id;
            }
            else
            {
                response.Success= false;
                response.ValidationErrors = new List<string>();
                foreach(var error in result.Errors)
                {
                    response.ValidationErrors.Add(error.Description);
                }
            }
            return response;
        }

        public async Task<BaseResponse> AddUserRole(AddUserRoleDto newRole)
        {
            var response = new BaseResponse();
            try
            {
               // var user = await db.Users.FirstOrDefaultAsync(x => x.Id == newRole.Id);
                var result = await db.UserRoles.AddAsync(new IdentityUserRole<int> { UserId = newRole.Id, RoleId = newRole.Role });
                db.SaveChangesAsync();
                response.Message = "Add new User Role Successfuly";
            }
            catch (Exception ex)
            {
                
                throw new Exception("Fail" + ex.Message);
            }
           
            return response;

        }

        public async Task<BaseResponse> DeleteUser(int Id)
        {
            var response = new BaseResponse();
            var user = await db.Users.FirstOrDefaultAsync(x=>x.Id==Id);
            if(user == null)
            {
                throw new NotFoundException(nameof(user),Id);
            }
            var result= await userManager.DeleteAsync(user);
            if(result.Succeeded)
            {
                response.Message = "Remove User successfully";
            }
            else
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in result.Errors)
                {
                    response.ValidationErrors.Add(error.Description);
                }
            }
            return response;
        }

        public async Task<BaseResponse> EditUesr(UserUpdateDto user)
        {
            BaseResponse response = new BaseResponse();
           // var q = await userManager.FindByIdAsync(user.Id.ToString());
            var userold = await db.Users.FirstOrDefaultAsync(x =>x.Id==user.Id);
            if (userold == null)
            {
                throw new NotFoundException(nameof(user), user.Id);
            }

            userold.FirstName = user.FirstName;
            userold.LastName = user.LastName;
            userold.Email = user.Email;
            userold.UserName = user.UserName;
            var result=await userManager.UpdateAsync(userold);
            if (result.Succeeded)
            {
                response.Message = "Update User successfully";
            }
            else
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in result.Errors)
                {
                    response.ValidationErrors.Add(error.Description);
                }
            }
            return response;
        }

        public async Task<List<UserListDto>> GetAllUser()
        {
            var users=await userManager.Users.Select(p=> new UserListDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                UserName = p.UserName,
                EmailConfirmed = p.EmailConfirmed,
                AccessFailedCount = p.AccessFailedCount
            }).AsNoTracking().ToListAsync();
            return users;
        }

        public async Task<UserListPageDto> GetByEmailUser(string email, int PageIndex, int PageSize)
        {
            var p = await db.Users.AsNoTracking().ToListAsync();
            if (email.IsNullOrEmpty())
            {
                p = await db.Users.Where(x => x.Email.Contains(email)).ToListAsync();
            }
            var users= p.Select(x => new UserListDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName,
                EmailConfirmed = x.EmailConfirmed,
                AccessFailedCount = x.AccessFailedCount
            }).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
            var q = new UserListPageDto
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                RecordCount = users.Count,
                UserListDto = users
            };
            return q;
        }

        public async Task<UserListDto> GetByIdUser(int Id)
        {
            var p = await db.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (p == null)
            {
                throw new NotFoundException("user", Id);
            }
            var user=new UserListDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                UserName = p.UserName,
                EmailConfirmed = p.EmailConfirmed,
                AccessFailedCount = p.AccessFailedCount
            };
            return user;
        }
    }
}
