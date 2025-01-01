using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string ThumbImage { get; set; } = null!;

    public int VendorId { get; set; }

    public int? CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public int? ChildCategoryId { get; set; }

    public int BrandId { get; set; }

    public int Qty { get; set; }

    public string ShortDescription { get; set; } = null!;

    public string LongDescription { get; set; } = null!;

    public string? VideoLink { get; set; }

    public string? Sku { get; set; }

    public double Price { get; set; }

    public double? OfferPrice { get; set; }

    public DateOnly? OfferStartDate { get; set; }

    public DateOnly? OfferEndDate { get; set; }

    public string? ProductType { get; set; }

    public int Status { get; set; }

    public int IsApproved { get; set; }

    public string? SeoTitle { get; set; }

    public string? SeoDescription { get; set; }

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ChildCategory? ChildCategory { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual ICollection<ProductImageGallery> ProductImageGalleries { get; set; } = new List<ProductImageGallery>();

    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    public virtual SubCategory? SubCategory { get; set; }
}
