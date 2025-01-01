using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class ProductVariant
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public int Status { get; set; }

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductVariantItem> ProductVariantItems { get; set; } = new List<ProductVariantItem>();
}
