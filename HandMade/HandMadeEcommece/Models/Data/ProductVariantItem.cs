using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class ProductVariantItem
{
    public int Id { get; set; }

    public int ProductVariantId { get; set; }

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public int IsDefault { get; set; }

    public int Status { get; set; }

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }

    public virtual ProductVariant ProductVariant { get; set; } = null!;
}
