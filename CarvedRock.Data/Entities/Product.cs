﻿using System.Text.Json.Serialization;

namespace CarvedRock.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string Category { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
        public ProductRating? Rating { get; set; }
    }
    [JsonSerializable(typeof(List<Product>))]
    public partial class CacheSourceGenerationContext : JsonSerializerContext
    {
    }
}
