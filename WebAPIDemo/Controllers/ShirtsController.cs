using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Filters.ActionFilters;
using WebAPIDemo.Filters.ExceptionFilters;
using WebAPIDemo.Models;
using WebAPIDemo.Models.Repositories;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(ShirtRepository.GetShirts());
        }
        [HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult GetShirtById(int id)//[FromRoute] => /id/colorName , [FromQuery] => ?color=red //[FromHeader(Name ="Color")] =>header(Postman In) da Color eklenecek
        {//IActionResult farklı şeyleri döndürebilmeyi(return) yarıyor
            //var shirt = shirts.FirstOrDefault(x => x.ShirtId == id);
            return Ok(ShirtRepository.GetShirtById(id));
        }
        [HttpPost]
        [Shirt_ValidateCreateShirtFilter]
        public IActionResult CreateShirt([FromBody]Shirt shirt)//[FromBody] => Body>raw(postman), [FromForm] => Body>form-data(postman)
        {
            //-----Tek Başına Class Yapmadan Önce---------- Shirt_ValidateCreateShirtFilterAttribute
            //if (shirt == null) { return BadRequest(); }
            //var existingShirt = ShirtRepository.GetShirtsByProperties(shirt.Brand, shirt.Gender, shirt.Color, shirt.Size);
            //if (existingShirt != null) return BadRequest();
            //-----Tek Başına Class Yapmadan Önce---------- Shirt_ValidateCreateShirtFilterAttribute

            ShirtRepository.AddShirt(shirt);

            return CreatedAtAction(nameof(GetShirtById),
                new { id = shirt.ShirtId },
                shirt);
        }
        [HttpPut("{id}")]
        [Shirt_ValidateShirtIdFilter]
        [Shirt_ValidateUpdateShirtFilterAttriibute]
        [Shirt_HandleUpdateExceptionsFilter] // After Action executed and then deleted ID, this exceptipn will work
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            ShirtRepository.UpdateShirt(shirt);

            return NoContent();
        }
        [HttpDelete("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult DeleteShirt(int id)
        {
            var shirt  = ShirtRepository.GetShirtById(id);
            ShirtRepository.DeleteShirt(id);
            return Ok(shirt);
        }
    }
}
