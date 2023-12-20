using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Attributes;
using WebAPIDemo.Data;
using WebAPIDemo.Filters.ActionFilters;
using WebAPIDemo.Filters.ActionFilters.V2;
using WebAPIDemo.Filters.AuthFilters;
using WebAPIDemo.Filters.ExceptionFilters;
using WebAPIDemo.Models;
using WebAPIDemo.Models.Repositories;

namespace WebAPIDemo.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/[controller]")]
    [JwtTokenAuthFilter]
    public class ShirtsController : ControllerBase      
    {
        private readonly ApplicationDbContext db;

        public ShirtsController(ApplicationDbContext Db)
        {
            db = Db;
        }

        [HttpGet]
        [RequiredClaim("read","true")]
        public IActionResult GetShirts()
        {
            // return Ok(ShirtRepository.GetShirts());
            return Ok(db.Shirts.ToList());
        } 
        [HttpGet("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        [RequiredClaim("read", "true")]
        public IActionResult GetShirtById(int id)//[FromRoute] => /id/colorName , [FromQuery] => ?color=red //[FromHeader(Name ="Color")] =>header(Postman In) da Color eklenecek
        {//IActionResult farklı şeyleri döndürebilmeyi(return) yarıyor
            //var shirt = shirts.FirstOrDefault(x => x.ShirtId == id);

            //var shirt = db.Shirts.Find(id); //Direk veritabanı
            return Ok(HttpContext.Items["shirt"]);
        }
        [HttpPost]
        [TypeFilter(typeof(Shirt_ValidateCreateShirtFilterAttribute))]
        [Shirt_EnsureDescriptionIsPresentFilter]
        [RequiredClaim("write", "true")]
        public IActionResult CreateShirt([FromBody]Shirt shirt)//[FromBody] => Body>raw(postman), [FromForm] => Body>form-data(postman)
        {
            //-----Tek Başına Class Yapmadan Önce---------- Shirt_ValidateCreateShirtFilterAttribute
            //if (shirt == null) { return BadRequest(); }
            //var existingShirt = ShirtRepository.GetShirtsByProperties(shirt.Brand, shirt.Gender, shirt.Color, shirt.Size);
            //if (existingShirt != null) return BadRequest();
            //-----Tek Başına Class Yapmadan Önce---------- Shirt_ValidateCreateShirtFilterAttribute

            //ShirtRepository.AddShirt(shirt);

            db.Shirts.Add(shirt);
            this.db.SaveChanges();

            return CreatedAtAction(nameof(GetShirtById),
                new { id = shirt.ShirtId },
                shirt);
        }
        [HttpPut("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        [Shirt_ValidateUpdateShirtFilterAttriibute]
        [TypeFilter(typeof(Shirt_HandleUpdateExceptionsFilterAttribute))] // After Action executed and then deleted ID, this exceptipn will work
        [Shirt_EnsureDescriptionIsPresentFilter]
        [RequiredClaim("write", "true")]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            var shirtToUpdate = HttpContext.Items["shirt"] as Shirt;
            shirtToUpdate.Brand = shirt.Brand;
            shirtToUpdate.Price = shirt.Price;
            shirtToUpdate.Size = shirt.Size;
            shirtToUpdate.Color = shirt.Color;
            shirtToUpdate.Gender = shirt.Gender;

            db.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        [RequiredClaim("delete", "true")]
        public IActionResult DeleteShirt(int id)
        {
            //var shirt  = ShirtRepository.GetShirtById(id);
            //ShirtRepository.DeleteShirt(id);
            var shirtToDeletre = HttpContext.Items["shirt"] as Shirt;
            db.Shirts.Remove(shirtToDeletre);
            db.SaveChanges();
            return Ok(shirtToDeletre);
        }
    }
}
