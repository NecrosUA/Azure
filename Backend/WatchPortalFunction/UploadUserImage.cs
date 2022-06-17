using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WatchPortalFunction
{
    public static class UploadUserImage
    {
        [FunctionName("UploadUserImage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "uploadimage")] HttpRequest req,
            ILogger log)
        {
            string Connection = Environment.GetEnvironmentVariable("ImageAzureWebJobsStorage");
            string containerName = Environment.GetEnvironmentVariable("ImageContainerName");
            //Guid id = Guid.NewGuid();
            var file = req.Form.Files["File"];
            string[] restr = file.FileName.Split('.');
            string filename = Guid.NewGuid() + "."+restr[restr.Length - 1]; //generate unique id of image

            Stream myBlob = file.OpenReadStream();

            var blobClient = new BlobContainerClient(Connection, containerName);
            var blob = blobClient.GetBlobClient(filename);
            await blob.UploadAsync(myBlob);
            return new OkObjectResult(filename);
        }
    }
}
