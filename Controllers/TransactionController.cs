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
    
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Transaction updated)
        {
            if (id != updated.Id) return BadRequest(new { message = "Transaction ID mismatch." });

            var transaction = _context.Transactions.Find(id);
            if (transaction == null) return NotFound(new { message = "Transaction not found." });

            if (updated.Amount <= 0 ||
                string.IsNullOrWhiteSpace(updated.Description) ||
                string.IsNullOrWhiteSpace(updated.Category))
            {
                return BadRequest(new { message = "Invalid transaction data." });
            }

            transaction.Amount = updated.Amount;
            transaction.Date = updated.Date;
            transaction.Description = updated.Description;
            transaction.Category = updated.Category;

            _context.SaveChanges();
            return Ok(transaction);
        }

        [HttpGet("summary/balance")]
        public IActionResult GetBalanceSummary()
        {
            var transactions = _context.Transactions.ToList();
            var income = transactions.Where(t => t.Amount > 0).Sum(t => t.Amount);
            var expenses = transactions.Where(t => t.Amount < 0).Sum(t => t.Amount);
            var balance = totalIncome + totalExpenses;   

            return Ok(new { TotalIncome = income, 
            TotalExpensesExpenses = expenses, 
            TotalBalance = balance });
        }

        [HttpGet("summary/month/{year}/{month}")]
        public IActionResult GetMonthlySummary(int year, int month)
        {
            var transactions = _context.Transactions
                .Where(t => t.Date.Year == year && t.Date.Month == month)
                .GroupBy(t => t.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(t => t.Amount)
                })
                .ToList();

            var netBalance = transactions.Sum(t => t.Total);

            return Ok(new
            {
                Month = $"{year}-{month:D2}",
                Categories = transactions,
                NetBalance = netBalance
            });
        }
}
