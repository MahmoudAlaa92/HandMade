namespace HandMadeEcommece.Models.Dto
{
    public class LogInUserModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
