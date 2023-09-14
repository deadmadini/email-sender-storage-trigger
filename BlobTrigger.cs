using System;
using System.IO;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmailSenderStorageTrigger
{
    [StorageAccount("BlobConnectionString")]
    public class BlobTrigger
    {
        [FunctionName("BlobEmailSender")]
        public void Run([BlobTrigger("files/{name}")]Stream myBlob, string name, ILogger log)
        {
            try
            {
                BlobServiceClient client = new(Environment.GetEnvironmentVariable("BlobConnectionString"));
                BlobContainerClient containerClient = client.GetBlobContainerClient(Environment.GetEnvironmentVariable("BlobContainer"));
                BlobClient blobClient = containerClient.GetBlobClient(name);

                BlobProperties properties = blobClient.GetProperties();
                string email = properties.Metadata["email"];

                EmailSender.SendSASToken(SasTokenGenerator.GenerateSasToken(name), email);

                log.LogInformation($"SAS token was successfully sent to {email}";
            }
            catch (Exception ex)
            {
                log.LogInformation("Exception: " + ex.Message);
            }
        }
    }
}
