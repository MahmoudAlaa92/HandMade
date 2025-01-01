using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class VendorCondition
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }
}
