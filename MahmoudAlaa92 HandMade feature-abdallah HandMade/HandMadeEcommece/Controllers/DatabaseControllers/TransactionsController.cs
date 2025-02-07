using HandMadeEcommece.Models;
using HandMadeEcommece.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Transactions;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {

        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> getallTransactions()
        {
            var transaction = await _context.Transactions.ToListAsync();
            if (!transaction.IsNullOrEmpty())
                return Ok(transaction);
            return NotFound($"No transaction is found :");

        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterTransaction(
                        [FromQuery] int? orderid, [FromQuery] int? userId, [FromQuery] int? companyId, 
                        [FromQuery] DateTime? date,[FromQuery ]  double? currencyAmount)
        
        {
            var transaction =await _context.Transactions.ToListAsync();

            if(orderid >0)
                transaction = await _context.Transactions.Where(t =>t.OrderId == orderid).ToListAsync();
            if(userId>0)
                transaction=await _context.Transactions.Where(t=>t.UserId==userId).ToListAsync();
            if(companyId>0)
                transaction = await _context.Transactions.Where(t => t.CompanyDeliveryId == companyId).ToListAsync();
            if(date !=null)
                transaction = await _context.Transactions.Where(t => t.CreatedAt == date).ToListAsync();
            if(currencyAmount!=0)
                transaction = await _context.Transactions.Where(t => t.AmountRealCurrency == currencyAmount).ToListAsync();


            if (transaction.IsNullOrEmpty())
            {
                return NotFound("no transactions yet");
            }

            return Ok(transaction);

        }
       
        
        [HttpPost]
        public async Task<IActionResult> newtransaction([FromBody] transactiondDto dto)
        {
            var newtransaction = new TransactionMoney
            {
                OrderId = dto.OrderId,
                UserId = dto.UserId,
                CompanyDeliveryId = dto.CompanyDeliveryId,
                PaymentMethod = dto.PaymentMethod,
                AmountRealCurrency = dto.AmountRealCurrency,
                Amount=dto.Amount,
                AmountRealCurrencyName = dto.AmountRealCurrencyName,
                CreatedAt =DateTime.Now,
            };

            await _context.Transactions.AddAsync(newtransaction);
            await _context.SaveChangesAsync();

            return Ok(newtransaction);
        }




        [HttpDelete]
        public async Task<IActionResult> deletetransaction([FromQuery] int id)
        {
           // var transaction = _context.Transactions.

                var transaction = await _context.Transactions
                                        .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
            
                return NotFound(new { message = "Transaction not found." });
            

            
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Transaction deleted successfully." });
        }






    }
}
