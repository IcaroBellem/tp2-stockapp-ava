namespace StockApp.Application.Interfaces
{
    public interface IMfaService
    {
        string GenerateOtp(string userId);
        bool ValidateOtp(string userOtp, string storedOtp);
    }
}
