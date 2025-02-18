using HandMadeEcommece.Models.Data;

namespace HandMadeEcommece.Models.Dto
{
    public class OrderDto
    {
        [Required]

        public int CompanyDeliveryId { get; set; }
        [Required]

        public int CartId { get; set; }
        [Required]

        public int UserId { get; set; }

        [Required]

        public CurrencyName CurrencyName { get; set; } = CurrencyName.EGP;

        [Required]

        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Paypal;
        [Required]

        public string OrderAddress { get; set; } = null!;

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        //public DeliveryCompany? DeliveryCompany { get; set; } 
        //public User? user { get; set; }
        //public TransactionMoney? transactionMoney { get; set; }
        //public Cart? Cart { get; set; }

    }
}
