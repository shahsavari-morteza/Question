using Quiz.Application.Models.Authentication;
using Quiz.Application.Models.Authentication.DTO.Users;
using Quiz.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Application.Contracts.Identity
{
    public interface IUserIdentityService
    {
        Task<List<UserListDto>> GetAllUser();
        Task<UserListPageDto> GetByEmailUser(string email, int PageIndex,int PageSize);
        Task<UserListDto> GetByIdUser(int Id);
        Task<AddNewUserResponse> AddNewUser(RegistrationRequest user);
        Task<BaseResponse> EditUesr(UserUpdateDto user);
        Task<BaseResponse> DeleteUser(int Id);
        Task<BaseResponse> AddUserRole(AddUserRoleDto newRole);


    }
}
