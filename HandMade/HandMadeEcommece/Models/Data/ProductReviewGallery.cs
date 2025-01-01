using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class ProductReviewGallery
{
    public int Id { get; set; }

    public int ProductReviewId { get; set; }

    public string Image { get; set; } = null!;

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }

    public virtual ProductReview ProductReview { get; set; } = null!;
}
