using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Username { get; set; }

    public string? Image { get; set; }

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

    public TimeOnly? EmailVerifiedAt { get; set; }

    public string Password { get; set; } = null!;

    public string? RememberToken { get; set; }

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
}
