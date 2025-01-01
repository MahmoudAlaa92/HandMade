using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class Transaction
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string TransactionId { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public double Amount { get; set; }

    public double AmountRealCurrency { get; set; }

    public string AmountRealCurrencyName { get; set; } = null!;

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;
}
