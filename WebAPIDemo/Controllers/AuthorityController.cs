using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Authority;

namespace WebAPIDemo.Controllers
{//This is for AUTHORİTY. Other Part(App(or USer) and Resource(WebApi))
    //Players: Application + AUTHORİTY(this) + Resource
    //Processes: Register>Authenticate>Create Token>Verify Token>Authorization
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        //public IActionResult Authenticate(string username, string password) //We do not want to go credentials through URL. So like below
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody]AppCredential credential)//So should be posted encrypted inside Https [frombody]
        {
            if(AppRepository.Authenticate(credential.ClientId, credential.Secret))
            {
                return Ok(new
                {
                    access_token = CreateToken(credential.ClientId),
                    expires_at = DateTime.UtcNow.AddMinutes(10)

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
        private string CreateToken(string clientId)
        {
            return string.Empty;
        }
    }
}
