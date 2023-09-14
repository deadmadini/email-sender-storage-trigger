using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace EmailSenderStorageTrigger
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([BlobTrigger("files/{name}", Connection = "3I8AbOpTdgeN4Tl+3VyTXnRBKkOHVxhFvarRz23t5BD0d3o6CEKdwbi07fEtnX1xw4ixnFWqAwEg+AStiVJkSg==")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
