namespace BatchDecorator.API.Models
{
    public class FTPConfigModel
    {
        public FTPConfigBaseModel Download { get; set; }
        public FTPConfigBaseModel Upload { get; set; }
    }

    public class FTPConfigBaseModel
    {
        public string Server { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string RemotePath { get; set; }
        public string LocalPath { get; set; }
    }
}
