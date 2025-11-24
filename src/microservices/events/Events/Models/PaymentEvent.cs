namespace Events.Models;

public class PaymentEvent
{
    public int PaymentId { get; set; }
    public int UserId { get; set; }
    public float Amount { get; set; }
    public string Status { get; set; } = null!;
    public DateTime Timestamp { get; set; }
    public string? MethodType { get; set; }
}