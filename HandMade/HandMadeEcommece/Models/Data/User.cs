using HandMadeEcommece.helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HandMadeEcommece.Models.Data;

public class User : AppUser
{

    public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    public ICollection<Chat> Chats { get; set; } = new List<Chat>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();  
    public ICollection<WishList> WishLists { get; set; } = new List<WishList>();
    public ICollection<Product> Products { get; set; }=new List<Product>();
    public ICollection<UserCoupons> UserCoupons { get; set; } = new List<UserCoupons>();
    public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
    public ICollection<PaypalSetting> PaypalSettings { get; set; } = new List<PaypalSetting>();
    public ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
    public ICollection<TransactionMoney> TransactionMoneys { get; set; } = new List<TransactionMoney>();
}
