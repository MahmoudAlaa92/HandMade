using HandMadeEcommece.helper;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace HandMadeEcommece.Models.Data
{
    public class Admin : AppUser
    {

        public decimal Salary { get; set; }
        public ICollection<Brand> brands { get; set; } = new List<Brand>();
        public ICollection<AdminBrand> adminBrands { get; set; } = new List<AdminBrand>();
        public ICollection<Category> categories { get; set; } = new List<Category>();
        public ICollection<AdminCategory>adminCategories { get; set; } = new List<AdminCategory>();
        public ICollection<UserAddress> addresses { get; set; } = new List<UserAddress>();
        public ICollection<AdminOrder> adminsOrders { get; set; } = new List<AdminOrder>();
        public ICollection<Order> orders { get; set; } = new List<Order>();
        public ICollection<Product> products { get; set; } = new List<Product>();
        public ICollection<AdminProduct> adminProducts { get; set; } = new List<AdminProduct>();
        public ICollection<AdminTransaction> adminTransactions { get; set; } = new List<AdminTransaction>();
        public ICollection<TransactionMoney> transactionMoneys { get; set; } = new List<TransactionMoney>();
        public ICollection<Vendor> vendors { get; set; } = new List<Vendor>();
        public ICollection<AdminVendor> adminVendors { get; set;} = new List<AdminVendor>();
        public ICollection<AdminVendorConditions> adminVendorConditions { get; set; } = new List<AdminVendorConditions>();
        public ICollection<VendorCondition>vendorConditions { get; set; } = new List<VendorCondition>();    
    }
}
