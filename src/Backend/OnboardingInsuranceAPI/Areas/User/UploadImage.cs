using Azure.Storage.Blobs;
using HttpMultipartParser;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.ErrorHandling;
using OnboardingInsuranceAPI.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class UploadImage : IHandler
{
    private readonly string _connection = Environment.GetEnvironmentVariable("ImageAzureWebJobsStorage");
    private readonly  string _containerName = Environment.GetEnvironmentVariable("ImageContainerName");

    public async Task<string> Handle(Stream stream)
    {
        var parsedFromBody = await MultipartFormDataParser.ParseAsync(stream);
        if (parsedFromBody is null)
            throw new ApiException(ErrorCode.UnhandledException);

        var file = parsedFromBody.Files[0];
        if (string.IsNullOrEmpty(file.Name))
            throw new ApiException(ErrorCode.UnhandledException);

        var splitName = file.FileName.Split('.');
        if (splitName.Length == 1)
            throw new ApiException(ErrorCode.UnhandledException);

        string filename = Guid.NewGuid() + "." + splitName[splitName.Length - 1]; //generate unique id of image
        using var myBlob = file.Data;
        var blobClient = new BlobContainerClient(_connection, _containerName);
        var blob = blobClient.GetBlobClient(filename);
        await blob.UploadAsync(myBlob);
        myBlob.Close();

        return filename;
    }
}
