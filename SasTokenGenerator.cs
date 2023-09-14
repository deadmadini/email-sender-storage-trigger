using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Sas;

namespace EmailSenderStorageTrigger
{
    internal static class SasTokenGenerator
    {
        internal static string GenerateSasToken(string fileName)
        {
            try
            {
                string azureStorageAccount = Environment.GetEnvironmentVariable("AzureStorageAccount");
                string azureStorageAccessKey = Environment.GetEnvironmentVariable("AzureStorageAccessKey");
                string blobContainer = Environment.GetEnvironmentVariable("BlobContainer");

                BlobSasBuilder blobSasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = blobContainer,
                    BlobName = fileName,
                    ExpiresOn = DateTime.UtcNow.AddHours(1),
                };

                blobSasBuilder.SetPermissions(BlobSasPermissions.Read);
                string sasToken = blobSasBuilder.ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(azureStorageAccount, azureStorageAccessKey)).ToString();
                return "https://storagekolesnyk.blob.core.windows.net/files/" + sasToken;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
