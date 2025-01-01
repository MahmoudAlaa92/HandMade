using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class OrderProduct
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? VendorId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Variants { get; set; } = null!;

    public int? VariantTotal { get; set; }

    public string UnitPrice { get; set; } = null!;

    public int Qty { get; set; }

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Vendor? Vendor { get; set; }
}
