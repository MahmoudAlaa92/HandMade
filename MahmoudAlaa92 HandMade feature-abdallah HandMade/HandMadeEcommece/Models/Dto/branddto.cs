namespace HandMadeEcommece.Models.Dto
{
    public class branddto
    {
        public int Id { get; set; }

        public byte[] Logo { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
