global using System.Linq.Dynamic;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using System.ComponentModel.DataAnnotations;
global using HandMadeEcommece.helper;
global using HandMadeEcommece.Models.Dto;
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
    }
}
