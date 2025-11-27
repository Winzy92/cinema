namespace ProxyService.Options;

public class ProxyOptions
{
    public const string Position = "ProxyOptions";
    
    public string MonolithUrl { get; set; } = "http://monolith:8080";
    public string MoviesServiceUrl { get; set; } = "http://movies-service:8081";
    public bool GradualMigration { get; set; } = false;
    public string MoviesMigrationPercent { get; set; } = "0"; 
}