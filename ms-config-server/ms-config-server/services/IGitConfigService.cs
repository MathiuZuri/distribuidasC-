namespace MsConfigService.Services
{
    public interface IGitConfigService
    {
        string GetConfiguration(string applicationName, string profile);
    }
}