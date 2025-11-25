using System.Text.Json.Serialization;

namespace Events.Models;

public class UserEvent
{
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("action")]
    public string Action { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
}