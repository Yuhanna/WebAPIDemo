using System.ComponentModel.DataAnnotations;
using WebAPIDemo.Models.Validations;


namespace WebAPIDemo.Models
{
    public class Shirt
    {
        public int ShirtId { get; set; }

        [Required]
        public string? Brand { get; set; }

        public string? Description { get; set; }//For example of versioning

        [Required]
        public string? Color { get; set; }

        [Shirt_EnsureCorrectSizing]
        public int? Size { get; set; }

        [Required]
        public string? Gender { get; set; }

        public double? Price { get; set; }
        public bool ValidateDescription()//For example of versioning
        {
            return !string.IsNullOrEmpty(Description);
        }
    }
}
