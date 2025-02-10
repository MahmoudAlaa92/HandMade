namespace HandMadeEcommece.Models.Dto
{
    public class CartItemDto
    {
        [Required]
        public int CartId { get; set; }
        [Required]
        public int Product_Variant_Item_Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]

        public decimal Price { get; set; }
    }
}
