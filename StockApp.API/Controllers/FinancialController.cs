using Microsoft.AspNetCore.Mvc;
using StockApp.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class FinancialController : ControllerBase
{
    private readonly IFinancialManagementService _service;

    public FinancialController(IFinancialManagementService service)
    {
        _service = service;
    }

    [HttpPost("add-transaction")]
    public async Task<IActionResult> AddTransaction([FromBody] FinancialTransaction transaction)
    {
        await _service.AddTransactionAsync(transaction);
        return Ok("Transaction added successfully.");
    }

    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions()
    {
        var transactions = await _service.GetTransactionsAsync();
        return Ok(transactions);
    }

    [HttpGet("report")]
    public async Task<IActionResult> GetReport()
    {
        var report = await _service.GenerateReportAsync();
        return Ok(report);
    }

    [HttpGet("transaction/{id}")]
    public async Task<IActionResult> GetTransaction(int id)
    {
        var transaction = await _service.GetTransactionByIdAsync(id);
        if (transaction == null) return NotFound();
        return Ok(transaction);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        await _service.DeleteTransactionAsync(id);
        return Ok("Transaction deleted successfully.");
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateTransaction([FromBody] FinancialTransaction transaction)
    {
        await _service.UpdateTransactionAsync(transaction);
        return Ok("Transaction updated successfully.");
    }
}
