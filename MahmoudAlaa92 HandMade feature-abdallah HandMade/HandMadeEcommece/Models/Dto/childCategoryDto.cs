namespace HandMadeEcommece.Models.Dto
{
    public class childCategoryDto
    {
        
            [Required]
            public int SubCategoryId { get; set; }

            [Required]
            
            public string Name { get; set; } = null!;

            [Required]
            public string Slug { get; set; } = null!;

            [Required]
            public int Status { get; set; }
        

    }
}
