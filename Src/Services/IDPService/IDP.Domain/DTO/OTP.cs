namespace IDP.Domain.DTO
{
    public class OTP
    {
        public required long UserId { get; set; }
        public required string OtpCode { get; set; }
        public bool IsUse { get; set; }
    }
}
