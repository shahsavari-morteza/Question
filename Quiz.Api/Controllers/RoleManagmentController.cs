using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.Application.Contracts.Identity;
using Quiz.Application.Models.Authentication;
using Quiz.Application.Models.Authentication.DTO.Roles;
using Quiz.Application.Models.Authentication.DTO.Users;
using Quiz.Application.Response;

namespace Quiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleManagmentController : ControllerBase
    {
        private readonly IRoleIdentityService repo;
        public RoleManagmentController(IRoleIdentityService repo)
        {
            this.repo = repo;
        }
        [HttpGet("all", Name = "GetAllRole")]
        public async Task<ActionResult<List<RoleListDto>>> GetAllRole()
        {
            var dots = await repo.GetAllRole();
            return Ok(dots);

        }

        [HttpGet("RoleDropDown", Name = "GetAllRoleDropDown")]
        public async Task<ActionResult<List<RoleDropDownDto>>> GetAllRoleDropDown()
        {
            var dots = await repo.GetAllRoleDropDown();
            return Ok(dots);

        }

        [HttpGet("{Id}", Name = "GetByIdRole")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<RoleListDto>> GetByIdRole(int Id)
        {
            var user = await repo.GetByIdRole(Id);
            return Ok(user);
        }




        [HttpPost(Name = "AddNewRole")]
        public async Task<ActionResult<AddNewRoleResponse>> AddNewRole(AddNewRoleDto role)
        {
            return Ok(await repo.AddNewRole(role));
        }
        [HttpPut(Name = "UpdateRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BaseResponse>> UpdateRole(RoleEditDto role)
        {
            return Ok(await repo.EditRole(role));
        }
        [HttpDelete("{id}", Name = "DeleteRole")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BaseResponse>> DeleteRole(int Id)
        {
            return Ok(await repo.DeleteRole(Id));
        }
    }
}
