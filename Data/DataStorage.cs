using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Growth_Strategy_Form_Recognizer.Data
{
    public class DataStorage : IDataStorage
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public DataStorage(string connectionString, string containerName)
        {
            _containerName = containerName;
            _blobServiceClient = new BlobServiceClient(connectionString);
        }
        public DataStorage(Uri connectionString, string containerName)
        {
            _containerName = containerName;
            _blobServiceClient = new BlobServiceClient(connectionString);
        }
        public async Task<BlobContainerClient?> GetContainerClient()
        {
            try
            {
                var blobContainer = _blobServiceClient.GetBlobContainerClient(_containerName);
                
                bool exist = await blobContainer.ExistsAsync();
                
                if (!exist)
                {
                    throw new Exception("Container doesn't exist");
                }

                return blobContainer;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<string> UploadFile(Stream localStream, string filename, string contentType = "application/pdf")
        {
            try
            {
                var containerClient = await GetContainerClient();

                if (containerClient == null)
                {
                    throw new Exception("Container doesn't exist");
                }
                
                BlobClient? blobClient = containerClient.GetBlobClient(filename);
                await blobClient.DeleteIfExistsAsync();
                
                var uploadOptions = new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = contentType
                    }
                };
                
                //var result = await containerClient.UploadBlobAsync(filename,localStream);
                var result = await blobClient.UploadAsync(localStream, uploadOptions);
                var blobUrl = containerClient.GetBlobClient(filename).Uri.ToString();
                return blobUrl;

            }catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
