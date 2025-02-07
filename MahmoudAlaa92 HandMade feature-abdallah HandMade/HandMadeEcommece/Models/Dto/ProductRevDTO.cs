namespace HandMadeEcommece.Models.Dto
{
    public class ProductRevDTO
    {

        [Required]
        public int ProductId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Review { get; set; } = null!;
        [Required]
        public string Rating { get; set; } = null!;

        public int Status { get; set; }

        public DateTime? CreatedAt { get; set; }

       
    }
}
