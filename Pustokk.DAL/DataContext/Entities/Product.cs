﻿using Pustokk.DAL.DataContext.Entities.Common;

namespace Pustokk.DAL.DataContext.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public decimal DisCountPrice { get; set; } 
    public int Count { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public ICollection<ProductImage> ProductImages { get; set; } = [];

    public ICollection<ProductTag> ProductTags { get; set; } = [];
    public   string? ProductCode { get; set; } 
    public  string? Brand { get; set; } 
    public bool Availability { get; set; } 
    public int RewardPoints { get; set; }
    public int SalesCount { get; set; }
}
