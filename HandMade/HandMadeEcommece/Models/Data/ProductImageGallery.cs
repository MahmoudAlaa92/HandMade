using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class ProductImageGallery
{
    public int Id { get; set; }

    public string Image { get; set; } = null!;

    public int ProductId { get; set; }

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
