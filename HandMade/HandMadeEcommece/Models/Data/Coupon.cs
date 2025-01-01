using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class Coupon
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int Quantity { get; set; }

    public int MaxUse { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string DiscountType { get; set; } = null!;

    public double Discount { get; set; }

    public int Status { get; set; }

    public int TotalUsed { get; set; }
}
