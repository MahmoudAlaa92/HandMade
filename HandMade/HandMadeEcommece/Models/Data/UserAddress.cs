using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class UserAddress
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string State { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public string Address { get; set; } = null!;

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
