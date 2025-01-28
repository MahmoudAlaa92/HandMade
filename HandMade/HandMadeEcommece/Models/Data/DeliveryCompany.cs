using System.ComponentModel.DataAnnotations;

namespace HandMadeEcommece.Models.Data
{
    public class DeliveryCompany
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [EmailAddress(ErrorMessage ="InValid Email")]
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Delivery_Zones { get; set; } = null!;
        public decimal Pricing {  get; set; }
        public string IdTax { get; set; } = null!;
        public byte[] Logo { get; set; } = null!;
        public ICollection<Order> orders { get; set; } = new List<Order>();
        public ICollection<TransactionMoney>transactionMoneys { get; set; } = new List<TransactionMoney>();
        public ICollection<PaypalSetting> paypalSetting { get; set; } = new List<PaypalSetting>();
    }
}
