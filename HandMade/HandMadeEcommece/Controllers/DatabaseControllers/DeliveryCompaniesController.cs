using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryCompaniesController : ControllerBase
    {
        private readonly AppDbContext _Context;
        public DeliveryCompaniesController(AppDbContext Context)
        {
            _Context = Context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeliveryCompaniesAll()
        {
            var deliveryCompanies = await _Context.DeliveryCompanies.ToListAsync();
            if (deliveryCompanies == null) return NotFound();
            return Ok(deliveryCompanies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeliveryCompany([FromForm] DeliveryCompanyDto deliveryCompanyDto)
        {
            if (!ModelState.IsValid || deliveryCompanyDto == null) return BadRequest();

            var deliveryCompany = new DeliveryCompany
            {
                Delivery_Zones = deliveryCompanyDto.Delivery_Zones,
                Email = deliveryCompanyDto.Email,
                Address = deliveryCompanyDto.Address,
                IdTax = deliveryCompanyDto.IdTax,
                Logo = await Methods.TransferImage(deliveryCompanyDto.Logo),
                Name = deliveryCompanyDto.Name,
                Pricing = deliveryCompanyDto.Pricing,
            };

            await _Context.DeliveryCompanies.AddAsync(deliveryCompany);
            await _Context.SaveChangesAsync();
            return Ok(deliveryCompany);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateDeliveryCompany(int id, DeliveryCompanyDto deliveryCompanyDto)
        {
            if (id <= 0 || deliveryCompanyDto == null) return BadRequest();

            var deliveryCompany = await _Context.DeliveryCompanies.FindAsync(id);
            if (deliveryCompany == null) return NotFound();
            deliveryCompany.Pricing = deliveryCompanyDto.Pricing;
            deliveryCompany.Delivery_Zones = deliveryCompanyDto.Delivery_Zones;
            deliveryCompany.Address = deliveryCompanyDto.Address;
            deliveryCompany.Email = deliveryCompanyDto.Email;
            deliveryCompany.Name = deliveryCompanyDto.Name;
            deliveryCompany.Logo = await Methods.TransferImage(deliveryCompanyDto.Logo);
            deliveryCompany.IdTax = deliveryCompanyDto.IdTax;
            _Context.DeliveryCompanies.Update(deliveryCompany);
            await _Context.SaveChangesAsync();
            return Ok(deliveryCompany);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteDeliveryCompany([FromQuery] List<int> ids)
        {
            if (ids == null || ids.Count <= 0) return BadRequest();
            var deliveryCompanies = new List<DeliveryCompany>();
            foreach (var id in ids)
            {
                var deliveryCompany = await _Context.DeliveryCompanies.FindAsync(id);
                if (deliveryCompany == null) continue;
                _Context.DeliveryCompanies.Remove(deliveryCompany);
            }
            await _Context.SaveChangesAsync();
            if (deliveryCompanies.Count == 0) return NotFound();
            return Ok(deliveryCompanies);
        }
    }
}
