using IDP.Domain.Entities.BaseEntities;

namespace IDP.Domain.Entities
{
    public class User:BaseEntity
    {
        //abstract
        public string? FullName { get; set; }
        public string? NationalCode { get; set; }
        public required string UserName { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public required string MobileNumber { get; set; }

    }
}
