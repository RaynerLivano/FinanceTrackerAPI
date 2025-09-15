using Microsoft.AspNetCore.Mvc;
using FinanceTrackerAPI.Data;
using FinanceTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("by-category/{category}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetByCategory(string category)
        {
            return await _context.Transactions
                .Where(t => t.Category == category)
                .ToListAsync();
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges(); 
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
