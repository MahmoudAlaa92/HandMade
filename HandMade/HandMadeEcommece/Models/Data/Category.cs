using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public int Status { get; set; }

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }

    public virtual ICollection<ChildCategory> ChildCategories { get; set; } = new List<ChildCategory>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
