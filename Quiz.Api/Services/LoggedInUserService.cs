using Quiz.Application.Contracts;
using System.Security.Claims;

namespace Quiz.Api.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
      private readonly IHttpContextAccessor _contextAccessor;
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            this._contextAccessor = httpContextAccessor;
        }
        public int UserId
        {
            get
            {
              return Convert.ToInt32( _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));
            }
        }
    }
}
