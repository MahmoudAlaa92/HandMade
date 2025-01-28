using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public class ProductReviewGallery
{
    public int Id { get; set; }

    public int ProductReviewId { get; set; }

    public byte[] Image { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public ProductReview ProductReview { get; set; } = null!;
}
