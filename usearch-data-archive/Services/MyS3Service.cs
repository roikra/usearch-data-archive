using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using ICSharpCode.SharpZipLib.GZip;

namespace data_archive
{
    public class MyS3Service
    {
        private readonly IAmazonS3 s3Client;
        private Configuration Configuration;

        public MyS3Service()
        {
            Configuration = new Configuration();

            s3Client = new AmazonS3Client(
                 Configuration.AWSAccessKeyId,
                Configuration.AWSSecretAccessKey,
                Configuration.Region);
        }

        public MyS3Service(Configuration configuration)
        {
            Configuration = configuration;

            s3Client = new AmazonS3Client(
                 Configuration.AWSAccessKeyId,
                Configuration.AWSSecretAccessKey,
                Configuration.Region);
        }

        public async Task GetObjectsList(string bucketName, string prefix, string outputFile = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(outputFile))
                {
                    File.WriteAllText(outputFile, "");
                }

                ListVersionsRequest request = new ListVersionsRequest()
                {
                    BucketName = bucketName,
                    Prefix = prefix,
                    // You can optionally specify key name prefix in the request
                    // if you want list of object versions of a specific object.

                    // For this example we limit response to return list of 2 versions.
                    MaxKeys = 2
                };
                do
                {
                    ListVersionsResponse response = await s3Client.ListVersionsAsync(request);
                    // Process response.
                    foreach (S3ObjectVersion entry in response.Versions)
                    {
                        //string line = string.Format("key = {0} size = {1}", entry.Key, entry.Size);
                        string line = string.Format("{0}", entry.Key);
                        Console.WriteLine(line);

                        if (!string.IsNullOrWhiteSpace(outputFile))
                        {
                            File.AppendAllLines(outputFile, new List<string> { line });
                        }
                    }

                    // If response is truncated, set the marker to get the next
                    // set of keys.
                    if (response.IsTruncated)
                    {
                        request.KeyMarker = response.NextKeyMarker;
                        request.VersionIdMarker = response.NextVersionIdMarker;
                    }
                    else
                    {
                        request = null;
                    }
                } while (request != null);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }

        public async Task<string> DowloadFileAsyncDecompress(string bucketName, string fileKey)
        {
            var fileTransferUtility = new TransferUtility(s3Client);

            using (Stream reader = await fileTransferUtility.OpenStreamAsync(bucketName, fileKey))
            {
                using (var gZipInputStream = new GZipInputStream(reader))
                using (var streamReader = new StreamReader(gZipInputStream))
                {
                    return await streamReader.ReadToEndAsync();
                }
            }
        }
    }
}