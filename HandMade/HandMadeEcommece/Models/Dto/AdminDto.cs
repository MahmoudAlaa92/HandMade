namespace HandMadeEcommece.Models.Dto
{
    public class AdminDto
    {
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        [Required]

        public string UserName { get; set; }

        public string image { get; set; }

        public string? Phone { get; set; }
        [Required]

        public string Password { get; set; } = null!;

        public double Salary {  get; set; }
    }
}
