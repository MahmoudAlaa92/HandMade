

namespace HandMadeEcommece.Models.Dto
{
    public class AdminDto
    {
        public IFormFile? image { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

        public string? UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        [Compare("Email")]
        public string ConfirmEmail {  get; set; }

        public string Phone { get; set; }


    }
}
