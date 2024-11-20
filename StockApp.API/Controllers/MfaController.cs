using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class MfaController : ControllerBase
{
    private readonly IMfaService _mfaService;

    public MfaController(IMfaService mfaService)
    {
        _mfaService = mfaService;
    }

    [HttpPost("generate")]
    public IActionResult GenerateOtp([FromBody] string userId)
    {
        var otp = _mfaService.GenerateOtp(userId);
        return Ok(new { Otp = otp });
    }

    [HttpPost("validate")]
    public IActionResult ValidateOtp([FromBody] ValidateOtpRequest request)
    {
        var isValid = _mfaService.ValidateOtp(request.UserId, request.UserOtp);
        return Ok(new { IsValid = isValid });
    }
}

public class ValidateOtpRequest
{
    public string UserId { get; set; }
    public string UserOtp { get; set; }
}
