namespace data_archive
{
    [Serializable]
    public class DocumentItem
    {
        public string id { get; set; }

        public string url { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string body { get; set; }

        public string keywords { get; set; }

        public string language { get; set; }

        public string datePublished { get; set; }

        public string provider { get; set; }

        public string mainImageUrl { get; set; }

        public string mainImageAlt { get; set; }

        public DocumentItem()
        {
        }
    }
}