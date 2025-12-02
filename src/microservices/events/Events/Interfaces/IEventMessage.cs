namespace Events.Interfaces;

public interface IEventMessage
{
    public Guid RequestId { get; set; }
}