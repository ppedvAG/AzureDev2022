﻿using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Category { get; set; }
        public decimal Price { get; set; }
    }
}
