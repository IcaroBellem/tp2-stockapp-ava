using StockApp.Domain.Entities;

public interface IFinancialManagementService
{
    Task AddTransactionAsync(FinancialTransaction transaction);
    Task<List<FinancialTransaction>> GetTransactionsAsync();
    Task<FinancialReportDTO> GenerateReportAsync();
    Task<FinancialTransaction?> GetTransactionByIdAsync(int id);
    Task DeleteTransactionAsync(int id);
    Task UpdateTransactionAsync(FinancialTransaction transaction);
}
