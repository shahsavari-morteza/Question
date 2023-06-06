using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.Application.Contracts.Identity;
using Quiz.Application.Models.Authentication;

namespace Quiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService auth;
        public AccountController(IAuthenticationService auth)
        {
            this.auth = auth;
        }
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await auth.AuthenticateAsync(request));
        }
        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
        {
            return Ok(await auth.RegisterAsync(request));
        }
        [HttpGet("LogOut")]
        public async Task<ActionResult> LogOut()
        {
            return Ok(auth.LogOut());
        }
    }
}
