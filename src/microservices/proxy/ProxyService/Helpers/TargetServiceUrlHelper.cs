namespace ProxyService.Helpers;

public static class TargetServiceUrlHelper
{
    public static string GetTargetServiceUrl(int moviesMigrationPercent, string microServiceUrl, string monolithUrl)
    {
        var random = new Random();
        return random.Next(100) < moviesMigrationPercent ? microServiceUrl : monolithUrl;
    }
}