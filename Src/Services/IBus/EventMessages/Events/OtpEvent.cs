namespace EventMessages.Events
{
    public class OtpEvent : BaseEvent
    {
        public string MobileNumber { get; set; }
        public string OtpCode { get; set; }
    }
}
