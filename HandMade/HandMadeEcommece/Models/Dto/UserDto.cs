using HandMadeEcommece.Models.Data;
using System.ComponentModel.DataAnnotations;

namespace HandMadeEcommece.Models.Dto
{
    public class UserDto
    {

        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;

        public string Username { get; set; }

        public IFormFile? image { get; set; }

        public string? Phone { get; set; }

        public string Email { get; set; } = null!;

        [Compare("Email")]
        public string ConfirmEmail { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
