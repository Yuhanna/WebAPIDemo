using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using WebAPIDemo.Authority;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace WebAPIDemo.Controllers
{//This is for AUTHORİTY. Other Part(App(or USer) and Resource(WebApi))
    //Players: Application + AUTHORİTY(this) + Resource
    //Processes: Register>Authenticate>Create Token>Verify Token>Authorization
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AuthorityController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        //public IActionResult Authenticate(string username, string password) //We do not want to go credentials through URL. So like below
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody]AppCredential credential)//So should be posted encrypted inside Https [frombody]
        {
            if(Authenticator.Authenticate(credential.ClientId, credential.Secret))
            {
                var expiresAt = DateTime.UtcNow.AddMinutes(10);
                return Ok(new
                {
                    access_token = Authenticator.CreateToken(credential.ClientId, expiresAt, configuration.GetValue<string>("SecretKey")),
                    expires_at = expiresAt

                });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "You are not authorized");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status401Unauthorized
                };
                return new UnauthorizedObjectResult(problemDetails);
            }
        }

    }
}
