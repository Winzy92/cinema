namespace Events.Options;

public class KafkaOptions
{
    public const string Position = "KafkaOptions";
    
    public string MonolithUrl { get; set; } = "http://monolith:8080";
    public string MoviesServiceUrl { get; set; } = "http://movies-service:8081";
    public bool GradualMigration { get; set; } = false;
    public int MoviesMigrationPercent { get; set; } = 0; 
}