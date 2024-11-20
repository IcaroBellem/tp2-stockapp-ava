using Microsoft.AspNetCore.Mvc;

public class BackupController : ControllerBase
{
    private readonly ICloudBackupService _backupService;

    public BackupController(ICloudBackupService backupService)
    {
        _backupService = backupService;
    }

    [HttpPost("backup")]
    public async Task<IActionResult> BackupFile(string filePath)
    {
        try
        {
            await _backupService.PerformBackupAsync(filePath);
            return Ok("Backup realizado com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao realizar backup: {ex.Message}");
        }
    }
}
