using HandMadeEcommece.Models.Data;

namespace HandMadeEcommece.Models.Dto
{
    public class Subcategory_DTO
    {
        

        public int CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public int Status { get; set; }

        public DateTime? CreatedAt { get; set; }

      

       
    }
}
