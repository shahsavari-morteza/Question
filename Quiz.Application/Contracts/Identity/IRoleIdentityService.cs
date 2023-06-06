using Quiz.Application.Response;
using Quiz.Application.Models.Authentication.DTO.Roles;

namespace Quiz.Application.Contracts.Identity
{
    public interface IRoleIdentityService
    {
        Task<List<RoleListDto>> GetAllRole();
        Task<List<RoleDropDownDto>> GetAllRoleDropDown();
        Task<RoleListDto> GetByIdRole(int Id);
        Task<AddNewRoleResponse> AddNewRole(AddNewRoleDto role);
        Task<BaseResponse> EditRole(RoleEditDto role);
        Task<BaseResponse> DeleteRole(int Id);
    }
}
