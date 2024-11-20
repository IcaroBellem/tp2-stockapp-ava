using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;


public class CloudBackupService : ICloudBackupService
{
    private readonly string _connectionString;
    private readonly string _containerName;

    public CloudBackupService(IConfiguration configuration)
    {
        _connectionString = configuration["AzureBlobStorage:ConnectionString"];
        _containerName = configuration["AzureBlobStorage:ContainerName"];
    }

    public async Task PerformBackupAsync(string filePath)
    {
        string fileName = Path.GetFileName(filePath);
        string backupFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{DateTime.UtcNow:yyyyMMddHHmmss}{Path.GetExtension(fileName)}";

        var blobServiceClient = new BlobServiceClient(_connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(backupFileName);

        using (var stream = File.OpenRead(filePath))
        {
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        Console.WriteLine($"Backup realizado com sucesso: {backupFileName}");
    }
}
