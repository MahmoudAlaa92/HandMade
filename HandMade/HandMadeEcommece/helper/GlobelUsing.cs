global using System.Linq.Dynamic;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using System.ComponentModel.DataAnnotations;
global using HandMadeEcommece.helper;
global using HandMadeEcommece.Models.Dto;
global using Microsoft.EntityFrameworkCore;
global using System.Net.Mail;
global using PhoneNumbers;
using System.Reflection.Metadata;



namespace HandMadeEcommece.helper
{
    public class Methods
    {
        public static async Task<byte[]> TransferImage(IFormFile Image)
        {
            if (Image == null || Image.Length == 0)
                return null;
            using (var dataStream = new MemoryStream())
            {
                await Image.CopyToAsync(dataStream);
                return dataStream.ToArray();
            }
        }

        public static Task<bool>IsValidEmail(string email)
        {
            try
            {
                var address =  new MailAddress(email);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
        public static Task<bool>IsValidPhone(string phone, string countryCode = "EG")
        {
            var phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
                var number = phoneUtil.Parse(phone, countryCode);
                return Task.FromResult(phoneUtil.IsValidNumber(number));
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
      
    }
}
