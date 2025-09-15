using Microsoft.AspNetCore.Mvc;
using FinanceTrackerAPI.Data;
using FinanceTrackerAPI.Models;

namespace FinanceTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var transactions = _context.Transactions.ToList();
            return Ok(transactions);
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges(); // saves to DB
            return CreatedAtAction(nameof(GetAll), transaction);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var transaction = _context.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
