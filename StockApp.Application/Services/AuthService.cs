using StockApp.Application.Interfaces;

public class AuthService
{
    private readonly IMfaService _mfaService;

    public AuthService(IMfaService mfaService)
    {
        _mfaService = mfaService;
    }

    public async Task<string> InitiateMfaAsync(string userId)
    {
        var otp = _mfaService.GenerateOtp(userId);
        return otp;
    }

    public bool ValidateMfa(string userId, string userOtp)
    {
        return _mfaService.ValidateOtp(userId, userOtp);
    }
}
