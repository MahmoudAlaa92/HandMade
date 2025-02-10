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
        [Required, StringLength(255),JsonIgnore]
        public string Password { get; set; }
        [Required,Compare("Password"),JsonIgnore]
        public string ConfirmPassword {  get; set; }
        [Required, StringLength(255)]
        public string Email { get; set; }
        [Required, StringLength(15)]
        public string Phone { get; set; }
        [Required]
        public int RoleId {  get; set; }
        public IFormFile? Image { get; set; }

    }
}
