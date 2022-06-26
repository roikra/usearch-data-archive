using Newtonsoft.Json;

namespace data_archive
{
    public class ArchiveMethods
    {
        /// <summary>
        /// Outputs a list of all objects in the bucket
        /// </summary>
        /// <param name="bucketName">The bucket name</param>
        /// <param name="directory">The diretcory name</param>
        /// <param name="outputfile">An optional file name to store the ouput list</param>
        /// <returns></returns>
        public async Task GetObjectsList(string bucketName,
          string directory,
          string outputfile = null)
        {
            await new MyS3Service().GetObjectsList(bucketName, directory, outputfile);
        }

        /// <summary>
        /// Extract the documents from a JSON file
        /// </summary>
        /// <param name="bucketName">The bucket name</param>
        /// <param name="fileKey">The file key</param>
        /// <returns></returns>
        public async Task<List<DocumentItem>> GetDocuments(string bucketName, string fileKey)
        {
            var documents = JsonConvert.DeserializeObject<List<DocumentItem>>(await DownloadFileAsJSON(bucketName, fileKey));

            return documents;
        }

        /// <summary>
        /// Download an object as a JSON string
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="fileKey"></param>
        /// <returns></returns>
        protected async Task<string> DownloadFileAsJSON(string bucketName, string fileKey)
        {
            return await new MyS3Service().DowloadFileAsyncDecompress(bucketName, fileKey);
        }
    }
}