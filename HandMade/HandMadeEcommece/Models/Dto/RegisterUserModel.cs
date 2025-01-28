namespace HandMadeEcommece.Models.Dto
{
    public class RegisterUserModel
    {
        [Required,StringLength(255)]
        public string FName {  get; set; }
        [Required, StringLength(255)]
        public string LName { get; set; }
        [Required, StringLength(255)]
        public string UserName {  get; set; }
        [Required, StringLength(255)]
        public string Password { get; set; }
        [Required, StringLength(255)]
        public string Email { get; set; }
        [Required,Compare("Email")]
        public string ConfirmEmail {  get; set; }
        [Required, StringLength(13)]
        public string Phone { get; set; }
        public IFormFile? Image { get; set; }

    }
}
