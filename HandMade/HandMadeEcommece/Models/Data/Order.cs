using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class Order
{
    public int Id { get; set; }

    public string InvocieId { get; set; } = null!;

    public int UserId { get; set; }

    public double SubTotal { get; set; }

    public double Amount { get; set; }

    public string CurrencyName { get; set; } = null!;

    public string CurrencyIcon { get; set; } = null!;

    public int ProductQty { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public int PaymentStatus { get; set; }

    public string OrderAddress { get; set; } = null!;

    public string ShppingMethod { get; set; } = null!;

    public string Coupon { get; set; } = null!;

    public string OrderStatus { get; set; } = null!;

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User User { get; set; } = null!;
}
