namespace HandMadeEcommece.Models.Dto
{
    public class CartItemDto
    {
        [Required]
        public int CartId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]

        public decimal Price { get; set; }
    }
}
