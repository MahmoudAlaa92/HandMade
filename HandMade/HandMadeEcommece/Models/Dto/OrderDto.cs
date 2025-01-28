using HandMadeEcommece.Models.Data;

namespace HandMadeEcommece.Models.Dto
{
    public class OrderDto
    {

        public int CompanyDeliveryId { get; set; }

        public int CartId { get; set; }

        public int UserId { get; set; }

        public double Amount { get; set; }// Amount from cart

        public string CurrencyName { get; set; } = null!;

        public int ProductQty { get; set; }

        public string PaymentMethod { get; set; } = null!;

        public int PaymentStatus { get; set; }

        public string OrderAddress { get; set; } = null!;

        public string ShoppingMethod { get; set; } = null!;

        public string OrderStatus { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DeliveryCompany? DeliveryCompany { get; set; } 
        public User? user { get; set; }
        public TransactionMoney? transactionMoney { get; set; }
        public Cart? Cart { get; set; }

    }
}
