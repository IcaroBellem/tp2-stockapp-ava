using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Entities;
using StockApp.Infra.Data.Context;

public class FinancialManagementService : IFinancialManagementService
{
    private readonly ApplicationDbContext _context;

    public FinancialManagementService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddTransactionAsync(FinancialTransaction transaction)
    {
        _context.FinancialTransactions.Add(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<List<FinancialTransaction>> GetTransactionsAsync()
    {
        return await _context.FinancialTransactions.ToListAsync();
    }

    public async Task<FinancialReportDTO> GenerateReportAsync()
    {
        var totalIncome = await _context.FinancialTransactions
            .Where(t => t.Type == "Income")
            .SumAsync(t => t.Amount);

        var totalExpenses = await _context.FinancialTransactions
            .Where(t => t.Type == "Expense")
            .SumAsync(t => t.Amount);

        return new FinancialReportDTO
        {
            TotalIncome = totalIncome,
            TotalExpenses = totalExpenses,
            NetProfit = totalIncome - totalExpenses
        };
    }

    public async Task<FinancialTransaction?> GetTransactionByIdAsync(int id)
    {
        return await _context.FinancialTransactions.FindAsync(id);
    }

    public async Task DeleteTransactionAsync(int id)
    {
        var transaction = await GetTransactionByIdAsync(id);
        if (transaction != null)
        {
            _context.FinancialTransactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateTransactionAsync(FinancialTransaction transaction)
    {
        var existingTransaction = await GetTransactionByIdAsync(transaction.Id);
        if (existingTransaction != null)
        {
            existingTransaction.Type = transaction.Type;
            existingTransaction.Amount = transaction.Amount;
            existingTransaction.Description = transaction.Description;
            existingTransaction.Date = transaction.Date;

            _context.FinancialTransactions.Update(existingTransaction);
            await _context.SaveChangesAsync();
        }
    }
}
