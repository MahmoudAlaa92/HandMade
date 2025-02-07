namespace HandMadeEcommece.Models.Data
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public decimal TotalPrice {  get; set; } // sum(cartitem of userId)
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int IsActived { get; set; }
        public User User { get; set; } = null!;
        public ICollection<CartItem> items { get; set; } = new List<CartItem>();
        public Order Order { get; set; } = null!;
    }
}
