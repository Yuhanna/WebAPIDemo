using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        [HttpGet]
        public string GetShirts()
        {
            return "Reading all the shirts";
        }
        [HttpGet("{id}")]
        public string GetShirtById(int id)//[FromQuery] => ?color=red //[FromHeader(Name ="Color")] =>header(Postman In) da Color eklenecek
        {
            return $"Reading shirts with ID= {id}";
        }
        [HttpPost]
        public string CreateShirt([FromBody]Shirt shirt)
        {
            return $"Creating a shirts";
        }
        [HttpPut("{id}")]
        public string UpdateShirt(int id)
        {
            return $"Updating shirts which ID= {id}";
        }
        [HttpDelete("{id}")]
        public string DeleteShirt(int id)
        {
            return $"Delete shirts which ID= {id}";
        }
    }
}
