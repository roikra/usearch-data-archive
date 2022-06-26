using Amazon;

namespace data_archive
{
    /// <summary>
    /// Edit this file: enter your credentials.
    /// </summary>
    public class Configuration
    {
        public string AWSAccessKeyId = "XXXXXXXXXXXXX";
        public string AWSSecretAccessKey = "XXXXXXXXXXXXXXX";
        public RegionEndpoint Region = RegionEndpoint.GetBySystemName("eu-central-1");
    }
}