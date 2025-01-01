using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models;

public partial class Brand
{
    public int Id { get; set; }

    public string Logo { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Status { get; set; } = null!;

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }
}
