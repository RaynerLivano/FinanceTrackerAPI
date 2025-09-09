using Microsoft.AspNetCore.Mvc;
using FinanceTrackerAPI.Models;

namespace FinanceTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private static readonly List<Transaction> Transactions = new();
        private static int Id = 1;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Transactions);
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            bool exits = Transactions.Any(t => 
            t.Description == transaction.Description && 
            t.Amount == transaction.Amount && 
            t.Date.Date == transaction.Date.Date);

            if (exits)
            {
                return Conflict("Transaction already exists.");
            }
            
            transaction.Id = Id++;
            Transactions.Add(transaction);
            return CreatedAtAction(nameof(GetAll), transaction);
        }

        [HttpDelete("clear")]
        public IActionResult Clear()
        {
            Transactions.Clear();
            return NoContent();
        }
    }
}
