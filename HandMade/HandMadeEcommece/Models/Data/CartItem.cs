using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace HandMadeEcommece.Models.Data
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId{get;set;}// reduce chosen product
        public int Quantity {  get; set; }

        public decimal Price { get; set; }
        public decimal SubTotal
        {
            get;set;
        }
        public Cart cart { get; set; } = null!;
        //public ProductVariantItem productVariantItem { get; set; } = null!;
        public Product product { get; set; } = null!;
    }
}
