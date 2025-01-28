using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public class UserAddress
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public int VendorId {  get; set; }  
    public int AdminId {  get; set; }

    public string Country { get; set; } = null!;

    public int State { get; set; }

    public string City { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public User User { get; set; } = null!;
    public Vendor Vendor { get; set; } = null!;
    public Admin Admin { get; set; } = null!;
}
