using Azure.Storage.Blobs;
using HttpMultipartParser;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.ErrorHandling;
using OnboardingInsuranceAPI.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class UploadImage : IHandler
{
    private readonly ILogger<UploadImage> _logger;
    private readonly string _connection = Environment.GetEnvironmentVariable("ImageAzureWebJobsStorage");
    private readonly  string _containerName = Environment.GetEnvironmentVariable("ImageContainerName");

    public UploadImage(ILogger<UploadImage> logger)
    {
        _logger = logger;
    }

    public async Task<string> Handle(FilePart file)
    {
        if (string.IsNullOrEmpty(file.Name))
        {
            _logger.LogWarning("Error, file is empty!");
            throw new ApiException(ErrorCode.UnhandledException);
        }

        var splitName = file.FileName.Split('.');

        if (splitName.Length == 1)
        {
            _logger.LogWarning("Error, wrong file!");
            throw new ApiException(ErrorCode.UnhandledException);
        }

        string filename = Guid.NewGuid() + "." + splitName[splitName.Length - 1]; //generate unique id of image
        Stream myBlob = file.Data;
        var blobClient = new BlobContainerClient(_connection, _containerName);
        var blob = blobClient.GetBlobClient(filename);
        await blob.UploadAsync(myBlob);

        return filename;
    }
}
