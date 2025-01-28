using HandMadeEcommece.Models.Data;

namespace HandMadeEcommece.Models.Dto
{
    public class ProductDto
    {

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public IFormFile ThumbImage { get; set; } = null!;

        public int VendorId { get; set; }

        public int? ChildCategoryId { get; set; }

        public int BrandId { get; set; }
        public int CouponId { get; set; }

        public int Qty { get; set; }

        public string ShortDescription { get; set; } = null!;

        public string LongDescription { get; set; } = null!;

        public string? VideoLink { get; set; }

        public string? Sku { get; set; }

        public double Price { get; set; }

        public double? OfferPrice { get; set; }

        public DateTime? OfferStartDate { get; set; }

        public DateTime? OfferEndDate { get; set; }

        public string? ProductType { get; set; }

        public int Status { get; set; }

        public int IsApproved { get; set; }

        public string? SeoTitle { get; set; }

        public string? SeoDescription { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}
