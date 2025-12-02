using System.Text.Json.Serialization;

namespace Events.Models;

public class MovieEvent
{
    [JsonPropertyName("movie_id")]
    public int MovieId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("action")]
    public string Action { get; set; }

    [JsonPropertyName("user_id")]
    public int? UserId { get; set; }

    [JsonPropertyName("rating")]
    public float? Rating { get; set; }

    [JsonPropertyName("genres")]
    public List<string>? Genres { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}