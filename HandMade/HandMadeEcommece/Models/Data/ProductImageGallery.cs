using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public class ProductImageGallery
{
    public int Id { get; set; }

    public byte[] Image { get; set; }

    public int ProductId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Product Product { get; set; } = null!;
}
