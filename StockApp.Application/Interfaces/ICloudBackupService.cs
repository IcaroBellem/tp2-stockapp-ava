public interface ICloudBackupService
{
    Task PerformBackupAsync(string filePath);
}
