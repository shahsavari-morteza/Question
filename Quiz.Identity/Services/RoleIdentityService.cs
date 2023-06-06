using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quiz.Application.Contracts.Identity;
using Quiz.Application.Exceptions;
using Quiz.Application.Models.Authentication.DTO.Roles;
using Quiz.Application.Response;
using Quiz.Identity.Models;
namespace Quiz.Identity.Services
{
    public class RoleIdentityService : IRoleIdentityService
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly QuizIdentityDbContext db;
        public RoleIdentityService(RoleManager<ApplicationRole> roleManager, QuizIdentityDbContext db)
        {
            this.roleManager = roleManager;
            this.db = db;
        }
        public async Task<AddNewRoleResponse> AddNewRole(AddNewRoleDto role)
        {
            AddNewRoleResponse response = new AddNewRoleResponse();
            ApplicationRole rolenew = new ApplicationRole
            {
                Name=role.Name,
                Description=role.Description,
            };
            var result = await roleManager.CreateAsync(rolenew);
            if (result.Succeeded)
            {

                response.Id =rolenew.Id;
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

        public async Task<BaseResponse> DeleteRole(int Id)
        {
            var response = new BaseResponse();
            var role = await db.Roles.FirstOrDefaultAsync(x => x.Id == Id);
            if (role == null)
            {
                throw new NotFoundException(nameof(role), Id);
            }
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
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

        public async Task<BaseResponse> EditRole(RoleEditDto role)
        {
            BaseResponse response = new BaseResponse();
            // var q = await userManager.FindByIdAsync(user.Id.ToString());
            var roleold = await db.Roles.FirstOrDefaultAsync(x => x.Id == role.RoleId);
            if (roleold == null)
            {
                throw new NotFoundException(nameof(role), role.RoleId);
            }

            roleold.Name = role.Name;
            roleold.Description=role.Description;
           
            var result = await roleManager.UpdateAsync(roleold);
            if (result.Succeeded)
            {
                response.Message = "Update Role successfully";
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

        public async Task<List<RoleListDto>> GetAllRole()
        {
            var roles = await roleManager.Roles.Select(x => new RoleListDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).AsNoTracking().ToListAsync();
            return roles;
        }

        public async Task<List<RoleDropDownDto>> GetAllRoleDropDown()
        {
            var roles = await roleManager.Roles.Select(x => new RoleDropDownDto
            {
                Id = x.Id,
                Name = x.Name,
               
            }).AsNoTracking().ToListAsync();
            return roles;
        }

        public async Task<RoleListDto> GetByIdRole(int Id)
        {
            var p = await db.Roles.FirstOrDefaultAsync(x =>x.Id  == Id);
            if (p == null)
            {
                throw new NotFoundException("Role", Id);
            }
            var role = new RoleListDto
            {
                Id = p.Id,
               Name=p.Name,
               Description = p.Description,
            };
            return role;
        }
    }
}
