namespace MsConfigService.Models
{
    public class GitConfigServerSettings
    {
        public string Uri { get; set; }
        public string[] SearchPaths { get; set; }
        public string DefaultLabel { get; set; }
    }
}