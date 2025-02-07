using HandMadeEcommece.Models.Data;
using System.ComponentModel.DataAnnotations;

namespace HandMadeEcommece.Models.Dto
{
    public class VendorDto
    {

        public string FName { get; set; } 
        public string LName { get; set; }

        public IFormFile? Image {  get; set; }

        public string Banner { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;


        [Compare("Email")]

        public string ConfirmEmail { get; set; } = null!;

        //public string Address { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? FbLink { get; set; }

        public string? TwLink { get; set; }

        public string? InstaLink { get; set; }

        public string? ShopName { get; set; }

        public int Status { get; set; }

    }
}
