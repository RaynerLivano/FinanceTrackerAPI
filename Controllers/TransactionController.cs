using Microsoft.AspNetCore.Mvc;
using FinanceTrackerAPI.Models;

namespace FinanceTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private static readonly List<Transaction> Transactions = new();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Transactions);
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            transaction.Id = Transactions.Count + 1;
            Transactions.Add(transaction);
            return CreatedAtAction(nameof(GetAll), transaction);
        }
    }
}
