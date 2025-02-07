using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace HandMadeEcommece.Models.Data
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int Product_Variant_Item_Id{get;set;}
        public int Quantity {  get; set; }

        public decimal Price { get; set; }
        public decimal SubTotal
        {
            get { return Price * Quantity; }
        }
        public Cart cart { get; set; } = null!;
        public ProductVariantItem productVariantItem { get; set; } = null!;

    }
}
