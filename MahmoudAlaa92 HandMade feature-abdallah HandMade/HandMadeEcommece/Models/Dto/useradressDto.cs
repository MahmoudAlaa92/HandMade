namespace HandMadeEcommece.Models.Dto
{
    public class useradressDto
    {

        [Required]
        public int UserId { get; set; }
        [Required]
        public int VendorId { get; set; }
        [Required]
        public int AdminId { get; set; }

        [Required]
        public string Country { get; set; } = null!;
        [Required]
        public int State { get; set; }
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string Zip { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}
