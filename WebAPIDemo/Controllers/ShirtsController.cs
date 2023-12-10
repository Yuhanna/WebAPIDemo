using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Filters;
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
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            try
            {
                ShirtRepository.UpdateShirt(shirt);
            }
            catch (Exception ex)
            {
                if (!ShirtRepository.ShirtExists(id))
                    return NotFound();

                throw;
            }


            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteShirt(int id)
        {
            return Ok("Delete shirts which ID= {id}");
        }
    }
}
