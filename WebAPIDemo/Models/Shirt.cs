﻿using System.ComponentModel.DataAnnotations;


namespace WebAPIDemo.Models
{
    public class Shirt
    {
        public int ShirtId { get; set; }

        [Required]
        public string? Brand { get; set; }

        [Required]
        public string? Color { get; set; }

        //[Shirt_EnsureCorrectSizingAttribute]
        public int? Size { get; set; }

        [Required]
        public string? Gender { get; set; }

        public double? Price { get; set; }
    }
}