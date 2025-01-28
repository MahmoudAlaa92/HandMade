using HandMadeEcommece.helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HandMadeEcommece.Models.Data;

public class Vendor : AppUser
{

    public string Banner { get; set; } = null!;
    public string Description { get; set; } = null!;

    public string ConfirmEmail { get; set; } = null!;

    public string? FbLink { get; set; }

    public string? TwLink { get; set; }

    public string? InstaLink { get; set; }

    public string? ShopName { get; set; }


    public ICollection<Chat> Chats { get; set; } = new List<Chat>();
    public ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
    public ICollection<Admin> admins { get; set; } = new List<Admin>();
    public ICollection<AdminVendor> adminsVendor { get; set; } = new List<AdminVendor>();
    public ICollection<OrderVendor> orderVendor { get; set; } = new List<OrderVendor>();
    public ICollection<Order> orders { get; set; } = new List<Order>();
    public ICollection<Product> products { get; set; } = new List<Product>();
    public ICollection<VendorProduct> vendorProducts { get; set; } = new List<VendorProduct>();
    public ICollection<PaypalSetting> paypalSettings { get; set; } = new List<PaypalSetting>();
    public ICollection<TransactionMoney>transactionMoney { get; set; } = new List<TransactionMoney>();
    public ICollection<VendorTransaction>vendorTransactions { get; set; } = new List<VendorTransaction>();
}
