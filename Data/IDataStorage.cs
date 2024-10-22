using Azure.Storage.Blobs;

namespace Growth_Strategy_Form_Recognizer.Data
{
    public interface IDataStorage
    {
        public Task<BlobContainerClient?> GetContainerClient();
        public Task<string?> UploadFile(Stream locaStream, string filename, string contentType);
    }
}
