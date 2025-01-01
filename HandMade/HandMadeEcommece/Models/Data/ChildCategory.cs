using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class ChildCategory
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int SubCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public int Status { get; set; }

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
