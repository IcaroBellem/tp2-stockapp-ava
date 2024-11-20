using Microsoft.Extensions.Caching.Memory;
using StockApp.Application.Interfaces;

public class MfaService : IMfaService
{
    private readonly IMemoryCache _cache;

    public MfaService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public string GenerateOtp(string userId)
    {
        var otp = new Random().Next(100000, 999999).ToString();
        _cache.Set(userId, otp, TimeSpan.FromMinutes(5));
        return otp;
    }

    public bool ValidateOtp(string userId, string userOtp)
    {
        if (_cache.TryGetValue(userId, out string storedOtp))
        {
            return userOtp == storedOtp;
        }
        return false;
    }
}
