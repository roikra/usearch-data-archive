namespace data_archive
{
    internal class Program
    {
        /// <summary>
        /// Choose an option and run
        /// Remember to add your credentials at Configuration.cs
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            int type = 0;

            if (args.Length > 0)
            {
                type = int.Parse(args[0]);
            }

            if (type == 0)
            {
                //Download a specific JSON file from an article directory
                string bucketName = "usearch-crawl-data";
                string fileKey = "articles-2022-06/06-26-2022-00000.json.gz"; //this is an example file to download

                Task.WaitAny(new ArchiveMethods().GetDocuments(bucketName, fileKey));
            }
            else if (type == 1)
            {
                //Download a specific JSON file from a webpage directory
                string bucketName = "usearch-crawl-data";
                string fileKey = "webpages-2022-06/file-000000000000.json.gz"; //this is an example file to download

                Task.WaitAny(new ArchiveMethods().GetDocuments(bucketName, fileKey));
            }
            else if (type == 2)
            {
                //View all of the directory's object lists or output to a file.
                string bucketName = "usearch-crawl-data";
                string directory = "articles-2022-06";
                string outputfile = @"C:\tmp\S3Archive\articles-2022-06.txt"; //this is an example output file; change accordingly

                Task.WaitAny(new ArchiveMethods().GetObjectsList(bucketName, directory, outputfile));
            }
            else if (type == 3)
            {
                //View all of the directory's object lists or output to a file.
                string bucketName = "usearch-crawl-data";
                string directory = "webpages-2022-06";
                string outputfile = @"C:\tmp\S3Archive\webpages-2022-06.txt"; //change accordingly

                Task.WaitAny(new ArchiveMethods().GetObjectsList(bucketName, directory, outputfile));
            }

            Console.WriteLine("Done!");
        }
    }
}