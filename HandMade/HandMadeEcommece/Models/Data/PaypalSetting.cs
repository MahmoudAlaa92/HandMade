using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class PaypalSetting
{
    public int Id { get; set; }

    public int Status { get; set; }

    public int Mode { get; set; }

    public string CountryName { get; set; } = null!;

    public string CurrencyName { get; set; } = null!;

    public double CurrencyRate { get; set; }

    public string ClientId { get; set; } = null!;

    public string SecretKey { get; set; } = null!;

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }
}
