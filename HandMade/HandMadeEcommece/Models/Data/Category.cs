using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public byte[]? Icon { get; set; }

    public int Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    public ICollection<Admin>admins { get; set; } = new List<Admin>();  
    public ICollection<AdminCategory> categories { get; set; } = new List<AdminCategory>();
}
