using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTbackendWithWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        //[Authorize]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            Console.WriteLine("this is authorized");
            Console.WriteLine(User.Identity.IsAuthenticated);
            Console.WriteLine(User.Identity.Name);
            return Ok(new { Message = "You are authorized!" , user=User.Identity.Name});
        }

        [AllowAnonymous]
        [HttpGet("public")]
        public IActionResult GetPublic()
        {
            return Ok(new { Message = "Anyone can see this." });
        }
    }
}
