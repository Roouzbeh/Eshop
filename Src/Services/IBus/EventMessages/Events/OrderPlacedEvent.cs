namespace EventMessages.Events
{
    public class OrderPlacedEvent:BaseEvent
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
