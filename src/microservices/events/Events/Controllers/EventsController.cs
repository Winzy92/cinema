using System.Text.Json;
using Events.Models;
using Events.Services;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly KafkaProducerService _producer;

    public EventsController(KafkaProducerService producer)
    {
        _producer = producer;
    }
    
    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        return Ok(new { status = true });
    }

    [HttpPost("movie")]
    public async Task<ActionResult> Movie([FromBody] MovieEvent evt)
    {
        var request = new MovieRequestMessage() { RequestId = Guid.NewGuid(), Event = evt };
        var result = await _producer.PublishAsync("movie-events", JsonSerializer.Serialize(request));
        return Created(string.Empty, new { status = "success", result.Partition, result.Offset, eventData = new Event(){Id = request.RequestId.ToString(), Timestamp = DateTime.UtcNow, Type = "movie"} });
    }

    [HttpPost("user")]
    public async Task<ActionResult<Event>> User([FromBody] UserEvent evt)
    {
        var request = new UserRequestMessage() { RequestId = Guid.NewGuid(), Event = evt };
        var result = await _producer.PublishAsync("user-events", JsonSerializer.Serialize(request));
        return Created(string.Empty, new { status = "success", result.Partition, result.Offset, eventData = new Event(){Id = request.RequestId.ToString(), Timestamp = DateTime.UtcNow, Type = "user"} });
    }

    [HttpPost("payment")]
    public async Task<ActionResult<Event>> Payment([FromBody] PaymentEvent evt)
    {
        var request = new PaymentEventMessage() { RequestId = Guid.NewGuid(), Event = evt };
        var result = await _producer.PublishAsync("payment-events", JsonSerializer.Serialize(request));
        return Created(string.Empty, new { status = "success", result.Partition, result.Offset, eventData = new Event(){Id = request.RequestId.ToString(), Timestamp = DateTime.UtcNow, Type = "payment"} });
    }
}