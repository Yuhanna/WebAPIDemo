﻿namespace WebAPIDemo.Models.Repositories
{
    public static class ShirtRepository
    {
        private static List<Shirt> shirts = new List<Shirt>()
        {
            new Shirt{ShirtId=1, Brand="My Brand", Color="Blue", Gender="Man", Price=30, Size=10},
            new Shirt{ShirtId=2, Brand="My Brand1", Color="Blue1", Gender="Man1", Price=130, Size=110},
            new Shirt{ShirtId=3, Brand="My Brand2", Color="Blue2", Gender="Man2", Price=230, Size=210},
            new Shirt{ShirtId=4, Brand="My Brand3", Color="Blue3", Gender="Man3", Price=330, Size=310},
        };

        public static List<Shirt> GetShirts()
        {
            return shirts;
        }

        public static bool ShirtExists(int id)
        {
            return shirts.Any(x => x.ShirtId == id);
        }
        public static Shirt? GetShirtById(int id)
        {
            return shirts.FirstOrDefault(x => x.ShirtId == id);
        }
        public static Shirt? GetShirtsByProperties(string? brand, string? gender, string? color, int? size)
        {
            return shirts.FirstOrDefault(x => 
            !string.IsNullOrWhiteSpace(brand) &&
            !string.IsNullOrWhiteSpace(x.Brand) &&
            x.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase)&&
            !string.IsNullOrWhiteSpace(gender) &&
            !string.IsNullOrWhiteSpace(x.Gender) &&
            x.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase) &&
            !string.IsNullOrWhiteSpace(color) &&
            !string.IsNullOrWhiteSpace(x.Color) &&
            x.Color.Equals(color, StringComparison.OrdinalIgnoreCase) &&
            size.HasValue && 
            x.Size.HasValue &&
            size.Value == x.Size.Value);
        }
        public static void AddShirt(Shirt shirt)
        {
            int maxId = shirts.Max(x => x.ShirtId);
            shirt.ShirtId = ++maxId;
            shirts.Add(shirt);
        }

        public static void UpdateShirt(Shirt shirt)
        {
            var shirtToUpdate = shirts.First(x => x.ShirtId == shirt.ShirtId);
            shirtToUpdate.Brand = shirt.Brand;
            shirtToUpdate.Price = shirt.Price;
            shirtToUpdate.Size = shirt.Size;
            shirtToUpdate.Color = shirt.Color;
            shirtToUpdate.Gender = shirt.Gender;
        }

    }
}