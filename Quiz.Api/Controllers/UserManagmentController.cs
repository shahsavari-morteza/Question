using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.Application.Contracts.Identity;
using Quiz.Application.Models.Authentication;
using Quiz.Application.Models.Authentication.DTO.Users;
using Quiz.Application.Response;

namespace Quiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagmentController : ControllerBase
    {
        private readonly IUserIdentityService repo;
        public UserManagmentController(IUserIdentityService repo)
        {
            this.repo = repo;
        }
        [HttpGet("all",Name ="GetAllUser")]
        public async Task<ActionResult<List<UserListDto>>> GetAllUser()
        {
            var dots=await repo.GetAllUser();
            return Ok(dots);
        }
        [HttpGet("{email}-{PageIndex}-{PageSize}", Name = "GetUserListPaging")]
        public async Task<ActionResult<UserListPageDto>> GetUserListPaging(string email, int PageIndex, int PageSize)
        {
            if (PageIndex < 1 || PageIndex == null)
            {
                PageIndex = 1;
            }
            if(PageSize==0||PageSize==null)
            {
                PageSize = 20;
            }
            var dots=await repo.GetByEmailUser(email, PageIndex, PageSize);
            return Ok(dots);
        }
        [HttpGet("{Id}", Name = "GetByIdUser")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserListDto>> GetByIdUser(int Id)
        {
            var user=await repo.GetByIdUser(Id);
            return Ok(user);
        }
        [HttpPost(Name = "AddNewUser")]
        public async Task<ActionResult<AddNewUserResponse>> AddNewUser(RegistrationRequest user)
        {
            return Ok(await repo.AddNewUser(user));
        }
        [HttpPut(Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BaseResponse>> UpdateUser(UserUpdateDto user)
        {
            return Ok(await repo.EditUesr(user));
        }
        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BaseResponse>> DeleteUser(int Id)
        {
            return Ok(await repo.DeleteUser(Id));
        }
        [HttpPost("Role",Name = "AddNewUserRole")]
        public async Task<ActionResult<BaseResponse>> AddNewUserRole(AddUserRoleDto newRole)
        {
            return Ok(await repo.AddUserRole(newRole));
        }
    }
}
