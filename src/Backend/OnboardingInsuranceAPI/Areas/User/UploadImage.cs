using Azure.Storage.Blobs;
using HttpMultipartParser;
using OnboardingInsuranceAPI.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class UploadImage : IHandler
{
    public async Task<string> SaveImageToBlobContainer(FilePart file)
    {
        string connection = Environment.GetEnvironmentVariable("ImageAzureWebJobsStorage");
        string containerName = Environment.GetEnvironmentVariable("ImageContainerName");

        string[] restr = file.FileName.Split('.');
        string filename = Guid.NewGuid() + "." + restr[restr.Length - 1]; //generate unique id of image

        Stream myBlob = file.Data;

        var blobClient = new BlobContainerClient(connection, containerName);
        var blob = blobClient.GetBlobClient(filename);
        await blob.UploadAsync(myBlob);

        return filename;
    }
}
