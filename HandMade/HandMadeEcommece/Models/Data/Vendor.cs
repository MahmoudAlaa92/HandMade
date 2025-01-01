using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class Vendor
{
    public int Id { get; set; }

    public string Banner { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? FbLink { get; set; }

    public string? TwLink { get; set; }

    public string? InstaLink { get; set; }

    public int UserId { get; set; }

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }

    public string? ShopName { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    public virtual User User { get; set; } = null!;
}
