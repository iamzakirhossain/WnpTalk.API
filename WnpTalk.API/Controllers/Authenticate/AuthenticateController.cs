using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WnpTalk.API.Functions.User;

namespace WnpTalk.API.Controllers.Authenticate
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private IUserFunction _userFunction;

        public AuthenticateController(IUserFunction userFunction)
        {
            _userFunction = userFunction;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate(AuthenticateRequest request)
        {
            var response = _userFunction.Authenticate(request.LoginId, request.Password);
            if (response == null)
                return BadRequest(new { StatusMessage = "Invalid username or password!" });

            return Ok(response);
        }
    }
}
