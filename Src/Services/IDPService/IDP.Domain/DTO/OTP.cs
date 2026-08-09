namespace IDP.Domain.DTO
{
    public class OTP
    {
        public required string UserName { get; set; }
        public required int OtpCode { get; set; }
        public bool IsUse { get; set; }
    }
}