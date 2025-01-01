using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class ProductReview
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public int VendorId { get; set; }

    public string Review { get; set; } = null!;

    public string Rating { get; set; } = null!;

    public int Status { get; set; }

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductReviewGallery> ProductReviewGalleries { get; set; } = new List<ProductReviewGallery>();

    public virtual User User { get; set; } = null!;

    public virtual Vendor Vendor { get; set; } = null!;
}
